namespace Veros.Paperless.Model.Servicos.Directory.FluxoAjuste
{
    using System;
    using System.IO;
    using System.Linq;
    using FluxoNormal;
    using Framework.IO;
    using Storages;
    using Veros.Data;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework;
    using Veros.Paperless.Model.Repositorios;

    public class PreparaInboxPosAjusteServico : IPreparaInboxPosAjusteServico
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;
        private readonly IRealizaGiroServico realizaGiroServico;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly IBaixarArquivoAmazonS3 baixarArquivoAmazonS3;

        public PreparaInboxPosAjusteServico(
            IUnitOfWork unitOfWork, 
            IDocumentoRepositorio documentoRepositorio, 
            IPaginaRepositorio paginaRepositorio, 
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico, 
            IRealizaGiroServico realizaGiroServico, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico, 
            IBaixarArquivoAmazonS3 baixarArquivoAmazonS3)
        {
            this.unitOfWork = unitOfWork;
            this.documentoRepositorio = documentoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
            this.realizaGiroServico = realizaGiroServico;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.baixarArquivoAmazonS3 = baixarArquivoAmazonS3;
        }

        public void Executar()
        {
            //// nao existe fila pra esse serviço. Nem vai existir. ele tem que rodar na mesma maquina onde está instalado o Server Manager, como instancia unica
            //// listar todos os mdocs que contem docs com docStatus 5B e mdocSatus 5B
            var listaDocumentos = this.unitOfWork.Obter(() => this.documentoRepositorio.ObterPendentesDeRecognitionServerPosAjuste());
            
            //// para cada Mdoc, baixar as paginas dentro da pasta 
            foreach (var documento in listaDocumentos)
            {
                this.unitOfWork.Transacionar(() =>
                {
                    try
                    {
                        this.BaixarPaginasParaProcessamentoDoRecognitionAbbyy(documento);

                        this.documentoRepositorio.MarcarInicioPosRecognitionService(documento.Id);

                        this.documentoRepositorio
                            .AlterarStatus(documento.Id, DocumentoStatus.StatusParaAguardandoReconhecimentoPosAjuste);

                        this.documentoRepositorio.ExcluirCapaTermoAutenticacaoPorLote(documento.Lote.Id);
                    }
                    catch (Exception exception)
                    {
                        if (exception.Message == "Documento Sem Paginas")
                        {
                            //// se estiver sem paginas, pode seguir no fluxo, não precisa esperar o job OutBoxPosAjuste
                            //// por enquanto, faz nada... necessário verificar se outros mdocs do lote precisam de ocr...                             
                        }
                        else
                        {
                            Log.Application.ErrorFormat(
                                exception,
                                "Erro ao processar job PosInbox para documento #{0}, no lote #{1}",
                                documento.Id,
                                documento.Lote.Id);                            
                        }
                    }
                });
            } 
        }

        private void BaixarPaginasParaProcessamentoDoRecognitionAbbyy(Documento documento)
        {
            //// antes de baixar verifica se o PDF outbox existe, e apaga ele (pode ter parado sistema da ultima vez)
            var arquivoPdfaMontadoPeloRecognition = Path.Combine(
                    Contexto.PastaOutboxPdfPosAjusteRecognition,
                    string.Format("LOTE_{0}_MDOC_{1}", documento.Lote.Id, documento.Id));

            if (Directory.Exists(arquivoPdfaMontadoPeloRecognition))
            {
                try
                {
                    Log.Application.DebugFormat("Excluindo diretorio outbox {0}. ", arquivoPdfaMontadoPeloRecognition);                    
                    Directory.Delete(arquivoPdfaMontadoPeloRecognition, true);
                }
                catch (Exception exception)
                {
                    Log.Application.ErrorFormat(exception, "Não foi possivel excluir diretorio Outbox {0}", arquivoPdfaMontadoPeloRecognition);
                }
            }

            var listaPaginas = this.paginaRepositorio.ObterPorDocumento(documento).Where(x => x.Status != PaginaStatus.StatusExcluida);

            if (listaPaginas.Any() == false)
            {
                Log.Application.WarnFormat("(job PosAjuste) Documento [{0}] não possui paginas válidas.", documento.Id);
                this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoErroPosInbox, documento.Id, string.Format("Pos Ajuste não realizado(s). Documento não possui paginas validas. Setado como excluído."));
                this.documentoRepositorio.AtualizaStatusDocumento(documento.Id, DocumentoStatus.Excluido);
                var pdfFilho = this.documentoRepositorio.ObterPdfFilho(documento);
                if (pdfFilho != null)
                {
                    this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoErroPosInbox, pdfFilho.Id, string.Format("PDF excluido devido a ausencia de paginas validas no orignal"));
                    this.documentoRepositorio.AtualizaStatusDocumento(documento.Id, DocumentoStatus.Excluido);
                }

                throw new Exception("Documento Sem Paginas");
            }

            var pathDestinoTempRaiz = Path.Combine(
                Contexto.PastaInboxPosAjusteRecognitionTemp, 
                string.Format("LOTE_{0}_MDOC_{1}", documento.Lote.Id, documento.Id));

            foreach (var pagina in listaPaginas)
            {
                var nomeArquivo = string.Format("{0:0000}_{1:000000000}.{2}", pagina.Ordem, pagina.Id, pagina.TipoArquivo.ToUpper());
                var pathDestinoTemp = Path.Combine(pathDestinoTempRaiz, nomeArquivo);
                try
                {
                    if (pagina.CloudOk)
                    {
                        var diretorio = Path.GetDirectoryName(pathDestinoTemp);
                        Directories.CreateIfNotExist(diretorio);

                        this.baixarArquivoAmazonS3.BaixarArquivo(pagina, pathDestinoTemp);
                    }
                    else
                    {
                        this.baixaArquivoFileTransferServico.BaixarArquivoNaPasta(pagina.Id, pagina.TipoArquivo, pathDestinoTemp, false);
                    }
                }
                catch (Exception exception)
                {
                    Log.Application.ErrorFormat("Arquivo não foi baixado do FT. Pagina #{0}. erro:{1}", pagina.Id, exception);
                    var mensagemErro = exception.Message;
                    if (exception.Message.Length > 200)
                    {
                        mensagemErro = exception.Message.Substring(0, 200);
                    }

                    this.gravaLogDaPaginaServico.Executar(LogPagina.AcaoErroDownloadFileTransfer, pagina.Id, documento.Id, string.Format("Erro FT durante download inbox OCR: {0}", mensagemErro));
                    throw;
                }
            }

            Log.Application.InfoFormat("Verificando necessidade de girar paginas em pasta temporaria para mdoc {0}", documento.Id);

            this.realizaGiroServico.Executar(documento, pathDestinoTempRaiz);

            var pathDestino = Path.Combine(Contexto.PastaInboxPosAjusteRecognition, string.Format("LOTE_{0}_MDOC_{1}", documento.Lote.Id, documento.Id));
            //// copiar para path de destino ABBYY
            var listaArquivos = Directory.GetFiles(pathDestinoTempRaiz, "*.*", SearchOption.AllDirectories);
            Log.Application.InfoFormat("Copiando para pasta PosInbox ABBYY. total de arquivos: {0}", listaArquivos.Length);
            foreach (string newPath in listaArquivos)
            {
                try
                {
                    Directories.CreateIfNotExist(pathDestino);
                    File.Copy(newPath, newPath.Replace(pathDestinoTempRaiz, pathDestino), true);
                }
                catch (Exception exception)
                {
                    var numeroDoc = Path.GetFileNameWithoutExtension(newPath);
                    var mensagemErro = exception.Message;
                    if (exception.Message.Length > 200)
                    {
                        mensagemErro = exception.Message.Substring(0, 200);
                    }

                    this.gravaLogDaPaginaServico.Executar(LogPagina.AcaoErroCopiarParaAbbyy, Convert.ToInt32(numeroDoc), documento.Id, string.Format("Erro ao copiar para ABBYY: {0}", mensagemErro));
                    throw;
                }
            }

            //// apagar pasta temp
            try
            {
                Log.Application.InfoFormat("Apagando pasta :{0}", pathDestinoTempRaiz);
                Directories.DeleteIfExist(pathDestinoTempRaiz);
            }
            catch (Exception exception)
            {
                Log.Application.ErrorFormat("Erro ao apagar pasta temp. Erro:{0}", exception);
            }
        }
    }
}
