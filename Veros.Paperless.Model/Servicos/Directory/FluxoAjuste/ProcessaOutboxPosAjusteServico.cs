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

    public class ProcessaOutboxPosAjusteServico : IProcessaOutboxPosAjusteServico
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly ICriaDocumentoPdfServico criaDocumentoPdfServico;
        private readonly IRemoverDocumentoFileTransfer removerDocumentoFileTransfer;

        public ProcessaOutboxPosAjusteServico(
            IUnitOfWork unitOfWork, 
            IDocumentoRepositorio documentoRepositorio, 
            IPaginaRepositorio paginaRepositorio, 
            IProcessoRepositorio processoRepositorio, 
            ICriaDocumentoPdfServico criaDocumentoPdfServico, 
            IRemoverDocumentoFileTransfer removerDocumentoFileTransfer)
        {
            this.unitOfWork = unitOfWork;
            this.documentoRepositorio = documentoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
            this.processoRepositorio = processoRepositorio;
            this.criaDocumentoPdfServico = criaDocumentoPdfServico;
            this.removerDocumentoFileTransfer = removerDocumentoFileTransfer;
        }

        public void Executar()
        {
            var listaDocumentos = this.unitOfWork.Obter(() => 
                this.documentoRepositorio.ObterProcessadosPeloRecognitionServerPosAjuste());

            Log.Application.InfoFormat(
                "Documentos aguardando termino do Recognition Server: {0}", 
                listaDocumentos.Count);
            
            //// para cada Mdoc, checar se ja tem PDF na pasta outbox. O doc ordem = 1 é sempre a folha de rosto. 
            foreach (var documentoComPng in listaDocumentos)
            {
                try
                {
                    var arquivoPdfaMontadoPeloRecognition = string.Empty;

                    if (this.PdfGeradoComSucesso(documentoComPng, ref arquivoPdfaMontadoPeloRecognition) == false)
                    {
                        Log.Application.DebugFormat(
                            "Documento {0} ainda não possui PDF/A finalizado. Pasta: {1}", 
                            documentoComPng.Id, 
                            arquivoPdfaMontadoPeloRecognition);

                        continue;
                    }
                        
                    Log.Application.InfoFormat(
                        "PDF/A do documento {0} encontrado! Pasta: {1}",
                        documentoComPng.Id, 
                        arquivoPdfaMontadoPeloRecognition);

                    this.unitOfWork.Transacionar(() =>
                    {
                        ////Cria o Mdoc, doc  e posta no FT
                        var processo = this.processoRepositorio.ObterPorLoteComDocumentos(documentoComPng.Lote.Id).FirstOrDefault();

                        try
                        {
                            var ordem = documentoComPng.Ordem;
                            ////var documentoPaiPdf = this.documentoRepositorio.ObterPorId(documentoComPng.DocumentoPaiId);
                            var novoDocPdf = this.criaDocumentoPdfServico.CriarNovoDocumentoPdf(
                                processo, 
                                documentoComPng, 
                                arquivoPdfaMontadoPeloRecognition, 
                                ordem);

                            novoDocPdf.DocumentoPaiId = documentoComPng.Id;
                            
                            novoDocPdf.TipoDocumento = new TipoDocumento { Id = documentoComPng.TipoDocumento.Id };

                            ////Avaliação para qual fase deve jogar o novo mdoc: classif ou CQ
                            this.DefinirFaseParaNovoPdf(novoDocPdf);

                            ////Excluindo documentos : PDF original, e mdoc com PNG´s
                            ////this.documentoRepositorio.AlterarStatus(documentoPaiPdf.Id, DocumentoStatus.Excluido);
                            this.documentoRepositorio.AlterarStatus(documentoComPng.Id, DocumentoStatus.Excluido);

                            this.paginaRepositorio.AlterarStatusPdfaProcessados(documentoComPng.Id);

                            this.documentoRepositorio.MarcarConcluidoRecognitionPosAjusteService(documentoComPng.Id);

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

                    ////this.unitOfWork.Transacionar(() => this.removerDocumentoFileTransfer.Executar(documentoComPng));
                }
                catch (Exception exception)
                {
                    var message = string.Format(
                        "Erro ao processar pasta Inbox para documento #{0}, no lote #{1}", 
                        documentoComPng.Id,
                        documentoComPng.Lote.Id);

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
                Contexto.PastaOutboxPdfPosAjusteRecognition, 
                string.Format("LOTE_{0}_MDOC_{1}", documento.Lote.Id, documento.Id));

            if (!Directory.Exists(arquivoPdfaMontadoPeloRecognition))
            {
                return false;
            }

            var arquivoPdf = Directory.GetFiles(arquivoPdfaMontadoPeloRecognition, "*.pdf");

            if (arquivoPdf.Length == 0)
            {
                return false;
            }

            arquivoPdfaMontadoPeloRecognition = Path.Combine(arquivoPdfaMontadoPeloRecognition, arquivoPdf[0]);

            return true;
        }
    }
}
