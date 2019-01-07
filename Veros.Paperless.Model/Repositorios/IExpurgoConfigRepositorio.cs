namespace Veros.Paperless.Model.Repositorios
{
    using Entidades;
    using Framework.Modelo;

    public interface IExpurgoConfigRepositorio : IRepositorioUnico<ExpurgoConfig>
    {
        void RegistrarUltimoExpurgoBk();

        void RegistrarUltimoExpurgoHistorico();
    }
}
