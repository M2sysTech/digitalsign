namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using Repositorios;
    using Veros.Paperless.Model.Entidades;

    public class BaixarPaginasDoDocumentoServico
    {
        private readonly IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico;
        private readonly IPaginaRepositorio paginaRepositorio;

        public BaixarPaginasDoDocumentoServico(
            IBaixaArquivoFileTransferServico baixaArquivoFileTransferServico, 
            IPaginaRepositorio paginaRepositorio)
        {
            this.baixaArquivoFileTransferServico = baixaArquivoFileTransferServico;
            this.paginaRepositorio = paginaRepositorio;
        }

        public void Executar(Documento documento, bool baixarOriginais = false)
        {
            foreach (var pagina in documento.Paginas)
            {
                this.baixaArquivoFileTransferServico.BaixarArquivo(
                    pagina.Id,
                    pagina.TipoArquivo,
                    baixarOriginais,
                    dataCenterId: pagina.DataCenter);
            }
        }

        public string BaixarPdf(int documentoId)
        {
            var pagina = this.paginaRepositorio.ObterPdfDocumento(documentoId);
            var arquivo = this.baixaArquivoFileTransferServico.BaixarArquivo(pagina.Id, pagina.TipoArquivo, dataCenterId: pagina.DataCenter);

            return arquivo;
        }
    }
}