namespace Veros.Paperless.Model.Servicos
{
    using Entidades;

    public interface IPacoteProcessadoFactory
    {
        PacoteProcessado ObterDoDia();
    }
}