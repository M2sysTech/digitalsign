namespace Veros.Paperless.Model.ViewModel
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;

    public class DocumentoVisualizadorViewModel
    {
        public DocumentoVisualizadorViewModel()
        {
            this.Paginas = new List<Pagina>();
        }

        public IList<Pagina> Paginas
        {
            get;
            set;
        }

        public static DocumentoVisualizadorViewModel Criar(Documento documento)
        {
            return new DocumentoVisualizadorViewModel
            {
                Paginas = documento.Paginas.ToList()
            };
        }

        public IList<Pagina> ObterPaginasOrdenadas()
        {
            var paginas = this.Paginas
               .OrderBy(x => x.Ordem)
               .ThenByDescending(x => x.Id).ToList();

            return paginas;
        }

        public void AdicionaPagina(Pagina pagina)
        {
            this.Paginas.Add(pagina);
        }
    }
}