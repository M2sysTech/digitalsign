namespace Veros.Paperless.Model.Storages
{
    public interface IStorageFileTransfer
    {
        string BaixarOriginal(int id, string tipoArquivo);

        void Postar(int id, string arquivo, string nomeArquivoDestino);
    }
}