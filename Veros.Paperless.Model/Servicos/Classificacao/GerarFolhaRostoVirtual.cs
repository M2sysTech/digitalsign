namespace Veros.Paperless.Model.Servicos.Classificacao
{
    using System;
    using System.IO;
    using System.Linq;
    using Entidades;
    using Framework;
    using Importacao;
    using Pdf;
    using Repositorios;

    public class GerarFolhaRostoVirtual : IGerarFolhaRostoVirtual
    {
        private readonly IDocumentoFabrica documentoFabrica;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPaginaFabrica paginaFabrica;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IPdfServico pdfServico;
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;
        private readonly IAssinarPdf assinarPdf;
        private readonly IPostaArquivoAmazonS3 postaArquivoAmazonS3;

        public GerarFolhaRostoVirtual(
            IDocumentoFabrica documentoFabrica, 
            IProcessoRepositorio processoRepositorio, 
            IDocumentoRepositorio documentoRepositorio, 
            IPaginaFabrica paginaFabrica, 
            IPaginaRepositorio paginaRepositorio, 
            IPdfServico pdfServico, 
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico, 
            IAssinarPdf assinarPdf, 
            IPostaArquivoAmazonS3 postaArquivoAmazonS3)
        {
            this.documentoFabrica = documentoFabrica;
            this.processoRepositorio = processoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.paginaFabrica = paginaFabrica;
            this.paginaRepositorio = paginaRepositorio;
            this.pdfServico = pdfServico;
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
            this.assinarPdf = assinarPdf;
            this.postaArquivoAmazonS3 = postaArquivoAmazonS3;
        }

        public void Executar(int processoId)
        {
            var processo = this.processoRepositorio.ObterDetalheDossie(processoId);

            var folhaDeRosto = this.documentoRepositorio.ObterFolhaDeRosto(processoId) ?? 
                this.documentoFabrica.CriarFolhaRosto(processo);

            folhaDeRosto.Status = DocumentoStatus.TransmissaoOk;
            
            this.documentoRepositorio.Salvar(folhaDeRosto);
            
            var pdf = this.pdfServico.GerarPdfFolhaRosto(processo);

            var paginas = this.paginaRepositorio.ObterPorDocumentoId(folhaDeRosto.Id);

            Pagina pagina;

            pagina = paginas.Any() == false ?
                this.paginaFabrica.Criar(folhaDeRosto, 0, pdf) : 
                paginas.First();

            this.paginaRepositorio.Salvar(pagina);

            if (pagina.CloudOk)
            {
                this.postaArquivoAmazonS3.PostarPagina(pagina, pdf);
            }
            else
            {
                this.postaArquivoFileTransferServico.PostarPagina(pagina, pdf);
            }

            try
            {
                File.Delete(pdf);
                File.Delete(string.Format("folharosto-{0}.PDF", processo.Id));
                File.Delete(string.Format("folharosto-{0}.jpg", processo.Id));
            }
            catch (Exception exception)
            {
                Log.Application.ErrorFormat("Erro ao tentar excluir arquivos de folha de rosto:: Isso não prejudica o processo como um todo, mas pode estar enchendo o disco. Motivo >> {0}. ", exception);
            }
        }
    }
}