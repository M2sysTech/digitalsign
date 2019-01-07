namespace Veros.Paperless.Model.Servicos.FileTransferBalance
{
    using Entidades;

    public interface IRegistraFileTransferDaPaginaServico
    {
        void Executar(FileTransfer fileTransfer, int paginaId);

        void RegistarConsumoBytes(FileTransfer fileTransfer);
    }
}
