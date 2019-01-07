namespace Veros.Paperless.Model.Servicos.Directory.FluxoAjuste
{
    using System;
    using System.IO;
    using System.Linq;
    using Framework.IO;
    using Veros.Data;
    using Veros.Paperless.Model.Servicos.Documentos;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework;
    using Veros.Paperless.Model.Repositorios;

    public class ProcessaOutboxAjusteBrancosServico : IProcessaOutboxAjusteBrancosServico
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly ICriaDocumentoPdfServico criaDocumentoPdfServico;

        public ProcessaOutboxAjusteBrancosServico(
            IUnitOfWork unitOfWork,
            IDocumentoRepositorio documentoRepositorio,
            IPaginaRepositorio paginaRepositorio,
            IProcessoRepositorio processoRepositorio,
            ICriaDocumentoPdfServico criaDocumentoPdfServico)
        {
            this.unitOfWork = unitOfWork;
            this.documentoRepositorio = documentoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
            this.processoRepositorio = processoRepositorio;
            this.criaDocumentoPdfServico = criaDocumentoPdfServico;
        }

        public void Executar()
        {
            var listaDocumentos = this.unitOfWork.Obter(() => 
                this.documentoRepositorio.ObterProcessadosPeloRecognitionServerAjusteBrancos());

            Log.Application.InfoFormat(
                "Documentos aguardando termino do Recognition Server: {0}", 
                listaDocumentos.Count);
            
            //// para cada Mdoc, checar se ja tem PDF na pasta outbox. O doc ordem = 1 é sempre a folha de rosto. 
            foreach (var documentoAguardandoPdf in listaDocumentos)
            {
                try
                {
                    var arquivoPdfaMontadoPeloRecognition = string.Empty;

                    if (this.PdfGeradoComSucesso(documentoAguardandoPdf, ref arquivoPdfaMontadoPeloRecognition) == false)
                    {
                        Log.Application.InfoFormat(
                            "Documento {0} ainda não possui PDF/A finalizado. Pasta: {1}", 
                            documentoAguardandoPdf.Id, 
                            arquivoPdfaMontadoPeloRecognition);

                        continue;
                    }
                        
                    Log.Application.InfoFormat(
                        "PDF/A do documento {0} encontrado! Pasta: {1}",
                        documentoAguardandoPdf.Id, 
                        arquivoPdfaMontadoPeloRecognition);

                    this.unitOfWork.Transacionar(() =>
                    {
                        var processo = this.processoRepositorio.ObterPorLoteComDocumentos(documentoAguardandoPdf.Lote.Id).FirstOrDefault();

                        try
                        {
                            var ordem = documentoAguardandoPdf.Ordem;
                            var documentoPaiPdf = this.documentoRepositorio.ObterPdfFilhoExcluido(documentoAguardandoPdf);

                            if (documentoPaiPdf == null)
                            {
                                documentoPaiPdf = this.documentoRepositorio.ObterPorId(documentoAguardandoPdf.Id);
                            }

                            var novoDocPdf = this.criaDocumentoPdfServico.CriarNovoDocumentoPdf(
                                processo, 
                                documentoAguardandoPdf, 
                                arquivoPdfaMontadoPeloRecognition, 
                                ordem);

                            if (documentoPaiPdf.DocumentoPaiId == 0)
                            {
                                novoDocPdf.DocumentoPaiId = documentoPaiPdf.Id;
                            }
                            else
                            {
                                novoDocPdf.DocumentoPaiId = documentoPaiPdf.DocumentoPaiId;
                            }

                            novoDocPdf.TipoDocumento = new TipoDocumento { Id = documentoPaiPdf.TipoDocumento.Id };
                            ////Avaliação para qual fase deve jogar o novo mdoc: classif ou CQ
                            this.DefinirFaseParaNovoPdf(novoDocPdf);
                            ////Excluindo documentos : PDF original, e mdoc com PNG´s
                            this.documentoRepositorio.AlterarStatus(documentoPaiPdf.Id, DocumentoStatus.Excluido);
                            this.documentoRepositorio.AlterarStatus(documentoAguardandoPdf.Id, DocumentoStatus.Excluido);
                            this.paginaRepositorio.AlterarStatusPdfaProcessados(documentoAguardandoPdf.Id);
                            
                            try
                            {
                                Directory.Delete(Path.GetDirectoryName(arquivoPdfaMontadoPeloRecognition), true);
                            }
                            catch (Exception exception)
                            {
                                Log.Application.ErrorFormat("Erro ao tentar excluir arquivo PDF:{0} : {1}", arquivoPdfaMontadoPeloRecognition, exception);
                            }
                        }
                        catch (Exception exception)
                        {
                            Log.Application.ErrorFormat("Erro ao criar MDoc/postar PDF:{0}", exception);
                        }
                    });
                }
                catch (Exception exception)
                {
                    var message = string.Format(
                        "Erro ao processar pasta Inbox para documento #{0}, no lote #{1}", 
                        documentoAguardandoPdf.Id,
                        documentoAguardandoPdf.Lote.Id);

                    Log.Application.Error(message, exception);
                    throw;
                }
            } 
        }

        private void DefinirFaseParaNovoPdf(Documento novoDocPdf)
        {
            if (novoDocPdf.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado)
            {
                novoDocPdf.Status = DocumentoStatus.AguardandoIdentificacao;
                return;
            }

            novoDocPdf.Status = DocumentoStatus.IdentificacaoConcluida;
        }

        private bool PdfGeradoComSucesso(Documento documento, ref string arquivoPdfaMontadoPeloRecognition)
        {
            arquivoPdfaMontadoPeloRecognition = Path.Combine(
                Contexto.PastaOutboxPdfRecognition, 
                string.Format("LOTE_{0}_MDOC_{1}", documento.Lote.Id, documento.Id));

            if (!Directory.Exists(arquivoPdfaMontadoPeloRecognition))
            {
                return false;
            }

            var arquivoPDF = Directory.GetFiles(arquivoPdfaMontadoPeloRecognition, "*.pdf");

            if (arquivoPDF.Length == 0)
            {
                return false;
            }

            //// verifica se há pasta inbox vazia, se houver, apaga. 
            try
            {
                this.ChecarExclusaoPastaInboxVazia(documento);
            }
            catch (Exception exception)
            {
                Log.Application.InfoFormat("Erro ao verificar pasta Inbox vazia : {0}", exception);
            }

            arquivoPdfaMontadoPeloRecognition = Path.Combine(arquivoPdfaMontadoPeloRecognition, arquivoPDF[0]);

            return true;
        }

        private void ChecarExclusaoPastaInboxVazia(Documento documento)
        {
            var pastaOrigem = Path.Combine(Contexto.PastaInboxRecognition, string.Format("LOTE_{0}_MDOC_{1}", documento.Lote.Id, documento.Id));
            if (Directory.Exists(pastaOrigem))
            {
                if (this.IsDirectoryEmpty(pastaOrigem))
                {
                    Directories.DeleteIfExist(pastaOrigem);
                }
            }
        }

        private bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
    }
}
