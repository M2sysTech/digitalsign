namespace Veros.Paperless.Model.Servicos.FileTransferBalance
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class RegistraFileTransferDaPaginaServico : IRegistraFileTransferDaPaginaServico
    {
        private readonly IFileTransferRepositorio fileTransferRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;

        public RegistraFileTransferDaPaginaServico(
            IFileTransferRepositorio fileTransferRepositorio, 
            IPaginaRepositorio paginaRepositorio)
        {
            this.fileTransferRepositorio = fileTransferRepositorio;
            this.paginaRepositorio = paginaRepositorio;
        }

        public void Executar(FileTransfer fileTransfer, int paginaId)
        {
            Log.Application.DebugFormat("Filetransfer: Registrando pagina #{0} postada no DC #{1}", paginaId, fileTransfer.ConfiguracaoIp.DataCenterId);

            if (fileTransfer.Id <= 0)
            {
                return;
            }

            ////this.fileTransferRepositorio.AtualizarEspacoUsado(fileTransfer.Id, fileTransfer.ArquivoAtualEmGigas());
            this.paginaRepositorio.AtualizarDataCenter(paginaId, fileTransfer.ConfiguracaoIp.DataCenterId);
        }

        public void RegistarConsumoBytes(FileTransfer fileTransfer)
        {
            if (fileTransfer.Id <= 0)
            {
                return;
            }

            this.fileTransferRepositorio.AtualizarEspacoUsado(fileTransfer.Id, fileTransfer.ArquivoAtualEmGigas());
        }
    }
}
