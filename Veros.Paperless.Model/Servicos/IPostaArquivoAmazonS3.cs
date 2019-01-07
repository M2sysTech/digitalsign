namespace Veros.Paperless.Model.Servicos
{
    using Entidades;

    public interface IPostaArquivoAmazonS3
    {
        void PostarPagina(Pagina pagina, string arquivoLocal);
    }
}
