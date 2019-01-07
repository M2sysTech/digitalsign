namespace Veros.Paperless.Model.ViewModel
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;

    public class DocumentoParaSeparacaoViewModel
    {
        public int Id { get; set; }

        public int Ordem { get; set; }

        public int TipoId { get; set; }

        public string TipoDescricao { get; set; }
        
        public IList<PaginaParaSeparacaoViewModel> Paginas { get; set; }

        public int NovaOrdem { get; set; }

        public string NomeArquivo { get; set; }

        public bool Manipulado { get; set; }

        public static DocumentoParaSeparacaoViewModel Criar(Documento documento)
        {
            return new DocumentoParaSeparacaoViewModel
            {
                Id = documento.Id,
                Ordem = documento.Ordem,
                TipoId = documento.TipoDocumento.Id,
                TipoDescricao = documento.TipoDocumento.Description,
                Paginas = new List<PaginaParaSeparacaoViewModel>()
            };
        }

        public void AdicionaPagina(Pagina pagina, int ordem)
        {
            var paginaParaSeparacaoViewModel = PaginaParaSeparacaoViewModel.Criar(pagina, ordem);
            paginaParaSeparacaoViewModel.DocumentoPaiNaTela = this.Id;
            this.Paginas.Add(paginaParaSeparacaoViewModel);
        }

        public string ListaDeImagens()
        {
            return this.Paginas.OrderBy(x => x.Ordem)
                .Join(";", pagina => string.Format("{0}.{1}", 
                    pagina.Id.ToString("000000000"), 
                    pagina.TipoArquivo.ToLower()));
        }
    }
}
