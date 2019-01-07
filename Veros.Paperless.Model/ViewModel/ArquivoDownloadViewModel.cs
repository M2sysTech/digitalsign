namespace Veros.Paperless.Model.ViewModel
{
    using System.Linq;
    using Entidades;

    public class ArquivoDownloadViewModel
    {
        public int DocumentoId { get; set; }

        public int LoteId { get; set; }

        public int PaginaId { get; set; }

        public string NomeArquivoOriginal { get; set; }

        public string Versao { get; set; }

        public string CaminhoDownload { get; set; }

        public static ArquivoDownloadViewModel Criar(Documento documento)
        {
            var documentoViewModel = new ArquivoDownloadViewModel
            {
                DocumentoId = documento.Id,
                LoteId = documento.Lote.Id,
                PaginaId = documento.Paginas.First().Id,
                NomeArquivoOriginal = documento.Paginas.First().NomeDoArquivo,
                CaminhoDownload = string.Format(@"/Images/{0:000000000}.{1}", documento.Paginas.First().Id, documento.Paginas.First().TipoArquivo.ToUpper()),
                Versao = documento.Versao
            };

            return documentoViewModel;
        }
    }
}
