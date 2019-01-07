namespace Veros.Paperless.Model.Repositorios
{
    using Entidades;
    using Framework.Modelo;

    public interface IFileTransferRepositorio : IRepositorio<FileTransfer>
    {
        void AtualizarEspacoUsado(int fileTransferId, double tamanhoNovoArquivo);

        FileTransfer ObterCloud();
    }
}