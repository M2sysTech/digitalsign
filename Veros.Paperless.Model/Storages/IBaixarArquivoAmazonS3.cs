namespace Veros.Paperless.Model.Storages
{
    using Entidades;

    public interface IBaixarArquivoAmazonS3
    {
        string ObterUrl(Pagina pagina);

        void BaixarArquivo(Pagina pagina, string caminhoArquivoDestino);
    }
}