namespace Veros.Paperless.Model.Servicos.FileTransferBalance
{
    using Entidades;
    
    public interface IObtemFileTransferDaPaginaServico
    {
        FileTransfer Obter(int paginaId, int dataCenterId);
    }
}
