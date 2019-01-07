namespace Veros.Paperless.Model.ViewModel
{
    using System.Collections.Generic;
    using Entidades;

    public class PaginasDoVisualizadorViewModel
    {
        public PaginasDoVisualizadorViewModel()
        {
            this.Imagens = new List<string>();
        }

        public string UrlBase { get; set; }

        public IList<string> Imagens { get; set; }

        public void AdicionaPagina(Pagina pagina)
        {
            var imagem = string.Format("{0}.{1}", 
                pagina.Id.ToString("000000000"), 
                pagina.TipoArquivo.ToLower());

            this.Imagens.Add(imagem);
        }
    }
}
