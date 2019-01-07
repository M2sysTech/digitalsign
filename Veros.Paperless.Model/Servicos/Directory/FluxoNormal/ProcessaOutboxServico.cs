namespace Veros.Paperless.Model.Servicos.Directory.FluxoNormal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Framework.IO;
    using Veros.Data;
    using Veros.Paperless.Model.Servicos.Documentos;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework;
    using Veros.Paperless.Model.Repositorios;

    public class ProcessaOutboxServico : IProcessaOutboxServico
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly ICriaDocumentoPdfServico criaDocumentoPdfServico;
        private readonly IRemoverDocumentoFileTransfer removerDocumentoFileTransfer;

        public ProcessaOutboxServico(IUnitOfWork unitOfWork,
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
            ////Nao existe fila pra esse serviço. Nem vai existir. ele tem que rodar na mesma maquina onde está instalado o Server Manager, como instancia unica
            ////Listar todos os mdocs que contem docs com docStatus 55 e mdocSatus 35
            var documentosProcessadosPeloRecognition = this.unitOfWork.Obter(() => this.documentoRepositorio.ObterProcessadosPeloRecognitionServer());
            Log.Application.InfoFormat("Documentos aguardando termino do Recognition Server: {0}", documentosProcessadosPeloRecognition.Count);

            var documentosPdfsGerados = new Dictionary<int, string>();

            Log.Application.InfoFormat("Pdfs Já gerados {0}", documentosPdfsGerados.Count);

            ////Para cada Mdoc, checar se ja tem PDF na pasta outbox. O doc ordem = 1 é sempre a folha de rosto. 
            foreach (var documento in documentosProcessadosPeloRecognition)
            {
                try
                {
                    var arquivoPdfaMontadoPeloRecognition = string.Empty;
                    if (this.PdfGeradoComSucesso(documento, ref arquivoPdfaMontadoPeloRecognition))
                    {
                        Log.Application.InfoFormat(
                            "PDF/A do documento {0} encontrado e processado! Pasta: {1}",
                            documento.Id, arquivoPdfaMontadoPeloRecognition);

                        this.unitOfWork.Transacionar(() =>
                        {
                            var processo = this.processoRepositorio
                                .ObterPorLoteComDocumentos(documento.Lote.Id).FirstOrDefault();

                            try
                            {
                                var ordem = documento.Ordem;

                                this.criaDocumentoPdfServico
                                    .CriarNovoDocumentoPdf(processo, documento, arquivoPdfaMontadoPeloRecognition, ordem);

                                this.paginaRepositorio.AlterarStatusPdfaProcessados(documento.Id);

                                this.documentoRepositorio.MarcarConcluidoRecognitionService(documento.Id);

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

                        ////this.unitOfWork.Transacionar(() => this.removerDocumentoFileTransfer.Executar(documento));
                    }
                    else
                    {
                        Log.Application.DebugFormat("Documento {0} ainda não possui PDF/A finalizado. Pasta: {1}", documento.Id, arquivoPdfaMontadoPeloRecognition);
                    }
                }
                catch (Exception exception)
                {
                    Log.Application.Error(string.Format("Erro ao processar pasta Inbox para documento #{0}, no lote #{1}", documento.Id, documento.Lote.Id), exception);
                    throw;
                }
            }
        }

        private bool PdfGeradoComSucesso(Documento documento, ref string arquivoPdfaMontadoPeloRecognition)
        {
            arquivoPdfaMontadoPeloRecognition = Path.Combine(
                Contexto.PastaOutboxPdfRecognition,
                string.Format("LOTE_{0}_MDOC_{1}", documento.Lote.Id, documento.Id));

            if (Directory.Exists(arquivoPdfaMontadoPeloRecognition) == false)
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

        /// <summary>
        /// Esse metodo estava gerando exceção no recognitionservice
        /// </summary>
        /// <param name="documento"></param>
        private void ChecarExclusaoPastaInboxVazia(Documento documento)
        {
            var pastaOrigem = Path.Combine(
                Contexto.PastaInboxRecognition,
                string.Format("LOTE_{0}_MDOC_{1}", documento.Lote.Id, documento.Id));

            var dataCriacaoDiretorio = new DirectoryInfo(pastaOrigem).CreationTime;

            var tempo = DateTime.Now.Subtract(dataCriacaoDiretorio);

            if (tempo.TotalMinutes < 20)
            {
                return;
            }

            if (Directory.Exists(pastaOrigem) == false)
            {
                return;
            }

            if (this.DiretorioEstaVazio(pastaOrigem) == false)
            {
                return;
            }

            Directories.DeleteIfExist(pastaOrigem);
        }

        private bool DiretorioEstaVazio(string path)
        {
            return Directory.EnumerateFileSystemEntries(path).Any() == false;
        }
    }
}
