namespace Veros.Paperless.Model.Servicos.ImagemOriginal
{
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class ObtemPaginaOriginalServico : IObtemPaginaOriginalServico
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public ObtemPaginaOriginalServico(IPaginaRepositorio paginaRepositorio, 
            IDocumentoRepositorio documentoRepositorio)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.documentoRepositorio = documentoRepositorio;
        }

        public Pagina Executar(int paginaId)
        {
            var pagina = this.paginaRepositorio.ObterComDocumento(paginaId);
            var documentoPai = this.documentoRepositorio.ObterPorId(pagina.Documento.DocumentoPaiId);
            var documentoAvo = this.documentoRepositorio.ObterComPaginas(documentoPai.DocumentoPaiId);

            return documentoAvo.Paginas.Where(x => x.Status != PaginaStatus.StatusExcluida).OrderBy(x => x.Ordem).ElementAt(pagina.Ordem - 1);
        }
    }
}
