namespace Veros.Paperless.Model.Servicos.Directory.FluxoNormal
{
    using System;
    using System.IO;
    using System.Linq;
    using Framework.IO;
    using Storages;
    using Veros.Data;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework;
    using Veros.Paperless.Model.Repositorios;

    public class PreparaInboxServico : IPreparaInboxServico
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;
        private readonly IRealizaGiroServico realizaGiroServico;
        private readonly IBaixarArquivoAmazonS3 baixarArquivoAmazonS3;

        public PreparaInboxServico(IUnitOfWork unitOfWork, 
            IDocumentoRepositorio documentoRepositorio, 
            IPaginaRepositorio paginaRepositorio, 
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico, 
            IRealizaGiroServico realizaGiroServico, 
            IBaixarArquivoAmazonS3 baixarArquivoAmazonS3)
        {
            this.unitOfWork = unitOfWork;
            this.documentoRepositorio = documentoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
            this.realizaGiroServico = realizaGiroServico;
            this.baixarArquivoAmazonS3 = baixarArquivoAmazonS3;
        }

        public void Executar()
        {
            ////Nao existe fila pra esse serviço. Nem vai existir. ele tem que rodar na mesma maquina onde está instalado o Server Manager, como instancia unica
            
            //// listar todos os mdocs que contem docs com docStatus 55 e mdocSatus 35
             var listaDocumentos = this.unitOfWork.Obter(() => this.documentoRepositorio.ObterPendentesDeRecognitionServer());
            
            //// para cada Mdoc, baixar as paginas dentro da pasta 
            foreach (var documento in listaDocumentos)
            {
                this.unitOfWork.Transacionar(() =>
                {
                    try
                    {
                        this.BaixarPaginasDocumentoParaRecognitionService(documento);
                        this.documentoRepositorio.MarcarInicioRecognitionService(documento.Id);
                        this.documentoRepositorio.AlterarStatus(documento.Id, DocumentoStatus.StatusParaReconhecimento);
                    }
                    catch (Exception exception)
                    {
                        Log.Application.Error(string.Format("Erro ao processar job Inbox para documento #{0}, no lote #{1}", documento.Id, documento.Lote.Id), exception);
                    }
                });
            } 
        }

        private void BaixarPaginasDocumentoParaRecognitionService(Documento documento)
        {
            var listaPaginas = this.paginaRepositorio.ObterPorDocumento(documento);

            if (listaPaginas.Count == 0)
            {
                throw new Exception(string.Format("Não há paginas para documento #{0}, Lote #{1}:", documento.Id, documento.Lote.Id));
            }

            Log.Application.InfoFormat("Baixando {0} paginas em pasta temporaria", listaPaginas.Count);

            var caminhoDestinoTemporario = Path.Combine(
                Contexto.PastaInboxRecognitionTemp, 
                string.Format("LOTE_{0}_MDOC_{1}", documento.Lote.Id, documento.Id));

            foreach (var pagina in listaPaginas.Where(x => x.Status != PaginaStatus.StatusExcluida))
            {
                var nomeArquivo = string.Format("{0:0000}_{1:000000000}.{2}", pagina.Ordem, pagina.Id, pagina.TipoArquivo.ToUpper());
                var pathDestinoTemp = Path.Combine(caminhoDestinoTemporario, nomeArquivo);
                try
                {
                    if (pagina.CloudOk)
                    {
                        Log.Application.DebugFormat("Pagina #{0} será baixada do cloud", pagina.Id);
                        var diretorio = Path.GetDirectoryName(pathDestinoTemp);
                        Directories.CreateIfNotExist(diretorio);

                        this.baixarArquivoAmazonS3.BaixarArquivo(pagina, pathDestinoTemp);
                    }
                    else
                    {
                        this.baixaArquivoFileTransferServico
                            .BaixarArquivoNaPasta(pagina.Id, pagina.TipoArquivo, pathDestinoTemp, false);
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

                    this.gravaLogDaPaginaServico.Executar(
                        LogPagina.AcaoErroDownloadFileTransfer, 
                        pagina.Id, 
                        documento.Id,
                        string.Format("Erro FT durante download inbox OCR: {0}", mensagemErro));

                    throw;
                }
            }

            Log.Application.InfoFormat("Verificando necessidade de girar paginas em pasta temporaria para mdoc {0}", documento.Id);
            
            this.realizaGiroServico.Executar(documento, caminhoDestinoTemporario);

            Log.Application.InfoFormat("copiando {0} paginas em pasta de trabalho da abbyy", listaPaginas.Count);

            var pathDestino = Path.Combine(Contexto.PastaInboxRecognition, string.Format("LOTE_{0}_MDOC_{1}", documento.Lote.Id, documento.Id));
            
            var listaArquivos = Directory.GetFiles(caminhoDestinoTemporario, "*.*", SearchOption.AllDirectories);
            Log.Application.DebugFormat("Copiando para pasta ABBYY. total de arquivos: {0}", listaArquivos.Length);

            foreach (string newPath in listaArquivos)
            {
                try
                {
                    Directories.CreateIfNotExist(pathDestino);
                    File.Copy(newPath, newPath.Replace(caminhoDestinoTemporario, pathDestino), true);
                }
                catch (Exception exception)
                {
                    var numeroDoc = Path.GetFileNameWithoutExtension(newPath);
                    var mensagemErro = exception.Message;
                    if (exception.Message.Length > 200)
                    {
                        mensagemErro = exception.Message.Substring(0, 200);
                    }

                    this.gravaLogDaPaginaServico.Executar(
                        LogPagina.AcaoErroCopiarParaAbbyy, 
                        Convert.ToInt32(numeroDoc),
                        documento.Id, 
                        string.Format("Erro ao copiar para ABBYY: {0}", mensagemErro));
                    throw;
                }
            }

            //// apagar pasta temp
            try
            {
                Directories.DeleteIfExist(caminhoDestinoTemporario);
            }
            catch (Exception exception)
            {
                Log.Application.ErrorFormat("Erro ao apagar pasta temp. Erro:{0}", exception);
            }
        }
    }
}
