namespace Veros.Paperless.Infra.Repositorios
{
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class LogPaginaRepositorio : Repositorio<LogPagina>, ILogPaginaRepositorio
    {
        public int ObterPorPagina(int paginaId)
        {
            throw new System.NotImplementedException();
        }
    }
}