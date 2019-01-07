namespace Veros.Paperless.Model.Servicos.FileTransferBalance
{
    using Veros.Paperless.Model.Entidades;

    public interface IObtemFileTransferParaArquivoServico
    {
        FileTransfer Obter(string arquivoLocal);

        FileTransfer Obter(int paginaId, string arquivoLocal);

        FileTransfer ObterMaisRecente();
    }
}
