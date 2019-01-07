namespace Veros.Paperless.Infra.Repositorios
{
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class FileTransferRepositorio : Repositorio<FileTransfer>, IFileTransferRepositorio
    {
        public void AtualizarEspacoUsado(int fileTransferId, double tamanhoNovoArquivo)
        {
            this.Session
              .CreateQuery("update FileTransfer set Usado = Usado + :tamanhoNovoArquivo where Id = :id")
              .SetParameter("tamanhoNovoArquivo", tamanhoNovoArquivo)
              .SetParameter("id", fileTransferId)
              .ExecuteUpdate();
        }

        public FileTransfer ObterCloud()
        {
            return this.Session.QueryOver<FileTransfer>()
                .Fetch(x => x.ConfiguracaoIp).Eager
                .Where(x => x.EhCloud)
                .SingleOrDefault();
        }
    }
}