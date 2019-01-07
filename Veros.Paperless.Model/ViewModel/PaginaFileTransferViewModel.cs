namespace Veros.Paperless.Model.ViewModel
{
    using Entidades;

    public class PaginaFileTransferViewModel
    {
        public int PaginaId { get; set; }

        public string TipoArquivo { get; set; }

        public bool Recapturado { get; set; }

        public bool PossuiThumbnail { get; set; }

        public bool CloudOk { get; set; }

        public FileTransfer FileTransfer { get; set; }

        public static PaginaFileTransferViewModel Criar(Pagina pagina, FileTransfer fileTransfer)
        {
            var viewModel = new PaginaFileTransferViewModel
            {
                PaginaId = pagina.Id,
                TipoArquivo = pagina.TipoArquivo,
                Recapturado = pagina.Recapturado == "S",
                FileTransfer = fileTransfer,
                PossuiThumbnail = pagina.PossuiThumbnail(),
                CloudOk = pagina.CloudOk
            };

            return viewModel;
        }
    }
}