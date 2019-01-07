namespace Veros.Paperless.Model.Servicos
{
    public interface IApagaArquivoFileTransferServico
    {
        void ApagarArquivo(int id, string fileType);
    }
}
