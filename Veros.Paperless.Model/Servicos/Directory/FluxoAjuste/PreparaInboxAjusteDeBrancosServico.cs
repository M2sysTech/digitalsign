namespace Veros.Paperless.Model.Servicos.Directory.FluxoAjuste
{
    using System;
    using System.IO;
    using System.Linq;
    using Framework.IO;
    using Veros.Data;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework;
    using Veros.Paperless.Model.Repositorios;

    public class PreparaInboxAjusteDeBrancosServico : IPreparaInboxAjusteDeBrancosServico
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IGravaLogDaPaginaServico gravaLogDaPaginaServico;

        public PreparaInboxAjusteDeBrancosServico(
            IUnitOfWork unitOfWork, 
            IDocumentoRepositorio documentoRepositorio, 
            IPaginaRepositorio paginaRepositorio, 
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico, 
            IGravaLogDaPaginaServico gravaLogDaPaginaServico)
        {
            this.unitOfWork = unitOfWork;
            this.documentoRepositorio = documentoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.gravaLogDaPaginaServico = gravaLogDaPaginaServico;
        }

        public void Executar()
        {
            //// nao existe fila pra esse serviço. Nem vai existir.
            //// ele tem que rodar na mesma maquina onde está instalado o Server Manager, como instancia unica
            
            //// listar todos os mdocs que contem docs com mdocSatus D0
            var listaDocumentos = this.unitOfWork.Obter(() => this.documentoRepositorio.ObterPendentesDeRecognitionServerAjusteDeBrancos());
            
            //// para cada Mdoc, baixar as paginas dentro da pasta 
            foreach (var documento in listaDocumentos)
            {
                this.unitOfWork.Transacionar(() =>
                {
                    try
                    {
                        this.BaixarPaginasParaProcessamentoDoRecognitionAbbyy(documento);

                        this.documentoRepositorio
                            .AlterarStatus(documento.Id, DocumentoStatus.StatusParaAguardandoReconhecimentoAjusteDeBrancos);
                    }
                    catch (Exception exception)
                    {
                        Log.Application.ErrorFormat(
                            exception,
                            "Erro ao processar pasta Inbox para documento #{0}, no lote #{1}", 
                            documento.Id,
                            documento.Lote.Id);

                        throw;
                    }
                });
            } 
        }

        private void BaixarPaginasParaProcessamentoDoRecognitionAbbyy(Documento documento)
        {
            var listaPaginas = this.paginaRepositorio.ObterPorDocumento(documento).Where(x => x.Status != PaginaStatus.StatusExcluida);

            if (listaPaginas.Any() == false)
            {
                throw new Exception(string.Format("Não há paginas para documento #{0}, Lote #{1}:", documento.Id, documento.Lote.Id));
            }

            var pathDestinoTempRaiz = Path.Combine(Contexto.PastaInboxRecognitionTemp, string.Format("LOTE_{0}_MDOC_{1}", documento.Lote.Id, documento.Id));
            foreach (var pagina in listaPaginas)
            {
                var nomeArquivo = string.Format("{0:000000000}.{1}", pagina.Id, pagina.TipoArquivo.ToUpper());
                var pathDestinoTemp = Path.Combine(pathDestinoTempRaiz, nomeArquivo);
                try
                {
                    this.baixaArquivoFileTransferServico.BaixarArquivoNaPasta(pagina.Id, pagina.TipoArquivo, pathDestinoTemp, false);
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

            var pathDestino = Path.Combine(Contexto.PastaInboxRecognition, string.Format("LOTE_{0}_MDOC_{1}", documento.Lote.Id, documento.Id));
            //// copiar para path de destino ABBYY
            var listaArquivos = Directory.GetFiles(pathDestinoTempRaiz, "*.*", SearchOption.AllDirectories);
            Log.Application.InfoFormat("Copiando para pasta Inbox ABBYY. total de arquivos: {0}", listaArquivos.Length);

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
                Directories.DeleteIfExist(pathDestinoTempRaiz);
            }
            catch (Exception exception)
            {
                Log.Application.ErrorFormat("Erro ao apagar pasta temp. Erro:{0}", exception);
            }
        }
    }
}
