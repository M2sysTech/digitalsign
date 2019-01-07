namespace Veros.Paperless.Model.Servicos.ImagemOriginal
{
    using Entidades;

    public interface IObtemPaginaOriginalServico
    {
        Pagina Executar(int paginaId);
    }
}
