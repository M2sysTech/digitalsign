namespace Veros.Paperless.Model.Servicos.Importacao
{
    using Veros.Paperless.Model.Entidades;

    public interface IPostaPaginaNoFileTransferServico
    {
        void Postar(Pagina pagina, string arquivo);
    }
}
