namespace DigitalSign.Jobs
{
    using System;
    using System.IO;
    using System.Threading;
    using Veros.Data;
    using Veros.Data.Jobs;
    using Veros.Framework;
    using Veros.Framework.IO;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Filas;
    using Veros.Paperless.Model.Repositorios;
    using Veros.Paperless.Model.Servicos;
    using Veros.Paperless.Model.Servicos.AssinaturaDigital;
    using Veros.Paperless.Model.Storages;

    public class SignJob : Job 
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFilaClienteGenerica filaClienteGenerica;
        private readonly IAssinarPdf assinarPdf;
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IFileSystem fileSystem;
        private readonly IVerificarPdf verificarPdf;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly IConsumoCarimboDigitalRepositorio consumoCarimboDigitalRepositorio;
        private readonly IBaixarArquivoAmazonS3 baixarArquivoAmazonS3;
        private readonly IPostaArquivoAmazonS3 postaArquivoAmazonS3;

        public SignJob(
            IUnitOfWork unitOfWork, 
            IFilaClienteGenerica filaClienteGenerica, 
            IAssinarPdf assinarPdf, 
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico, 
            IPaginaRepositorio paginaRepositorio, 
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico, 
            IDocumentoRepositorio documentoRepositorio, 
            IFileSystem fileSystem, 
            IVerificarPdf verificarPdf, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico, 
            IConsumoCarimboDigitalRepositorio consumoCarimboDigitalRepositorio, 
            IBaixarArquivoAmazonS3 baixarArquivoAmazonS3, 
            IPostaArquivoAmazonS3 postaArquivoAmazonS3)
        {
            this.unitOfWork = unitOfWork;
            this.filaClienteGenerica = filaClienteGenerica;
            this.assinarPdf = assinarPdf;
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
            this.paginaRepositorio = paginaRepositorio;
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.documentoRepositorio = documentoRepositorio;
            this.fileSystem = fileSystem;
            this.verificarPdf = verificarPdf;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.consumoCarimboDigitalRepositorio = consumoCarimboDigitalRepositorio;
            this.baixarArquivoAmazonS3 = baixarArquivoAmazonS3;
            this.postaArquivoAmazonS3 = postaArquivoAmazonS3;
        }

        public override void Execute()
        {
            var documentoId = this.ObterDocumentoParaAssinatura();
            
            if (documentoId == 0)
            {
                return;
            }

            string pdfAssinado = null;
            string pdf = null;

            try
            {
                this.unitOfWork.Transacionar(() =>
                {
                    var pagina = this.paginaRepositorio.ObterPdfDocumento(documentoId);

                    if (pagina.CloudOk == false)
                    {
                        pdf = this.baixaArquivoFileTransferServico.BaixarArquivo(pagina.Id, pagina.TipoArquivo, dataCenterId: pagina.DataCenter, usarCache: false);
                    }
                    else
                    {
                        var fileName = string.Format("{0}.{1}", pagina.Id.ToString("000000000"), pagina.TipoArquivo);
                        pdf = Path.Combine(Aplicacao.Caminho, "Images", fileName);

                        this.baixarArquivoAmazonS3.BaixarArquivo(pagina, pdf);
                    }

                    Log.Application.Debug("DataCenter " + pagina.DataCenter);
                    if (this.verificarPdf.EstaAssinado(pdf) == false)
                    {
                        Log.Application.Info("Pdf não está assinado... ");
                        pdfAssinado = this.assinarPdf.Execute(pdf);

                        var consumoCarimboDigital = ConsumoCarimboDigital.Criar(documentoId, pagina.Lote.Id);
                        this.consumoCarimboDigitalRepositorio.Salvar(consumoCarimboDigital);

                        if (pagina.CloudOk)
                        {
                            this.postaArquivoAmazonS3.PostarPagina(pagina, pdfAssinado);
                        }
                        else
                        {
                            this.postaArquivoFileTransferServico.PostarPagina(pagina, pdfAssinado);
                        }
                    }

                    this.documentoRepositorio.AlterarStatus(documentoId, DocumentoStatus.Assinado);
                    
                    this.gravaLogDoDocumentoServico.Executar(
                        LogDocumento.AcaoAssinaturaDigital,
                        documentoId,
                        "Documento assinado com sucesso");

                    Log.Application.InfoFormat("Documento {0} assinado com sucesso", documentoId);
                });
            }
            catch (RepositorioCertificadosException exception)
            {
                Log.Application.Info(
                    "Repositorio de certificados não disponivel. uma nova tentativa será realizada depois de 2 minutos");

                Thread.Sleep(TimeSpan.FromMinutes(2));
            }
            catch (Exception exception)
            {
                this.unitOfWork.Transacionar(() => 
                    this.documentoRepositorio.AlterarStatus(documentoId, DocumentoStatus.ErroAoAssinar));

                Log.Application.Error(exception);

                throw;
            }
            finally
            {
                if (string.IsNullOrEmpty(pdfAssinado) == false && this.fileSystem.Exists(pdfAssinado))
                {
                    this.fileSystem.DeleteFile(pdfAssinado);
                    Log.Application.Info("Pdf assinado foi removido do espaço temporario");
                }

                if (string.IsNullOrEmpty(pdf) == false && this.fileSystem.Exists(pdf))
                {
                    this.fileSystem.DeleteFile(pdf);
                    Log.Application.Info("Pdf Original foi removido do espaço temporario");
                }
            }
        }

        private int ObterDocumentoParaAssinatura()
        {
            try
            {
                return this.unitOfWork.Obter(() => 
                    this.filaClienteGenerica.ObterProximo(TiposDeFila.AssinaturaDigital));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}