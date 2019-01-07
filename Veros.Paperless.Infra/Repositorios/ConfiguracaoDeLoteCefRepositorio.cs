namespace Veros.Paperless.Infra.Repositorios
{
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class ConfiguracaoDeLoteCefRepositorio : Repositorio<ConfiguracaoDeLoteCef>, IConfiguracaoDeLoteCefRepositorio
    {
        public ConfiguracaoDeLoteCef ObterUnico()
        {
            return this.Session.QueryOver<ConfiguracaoDeLoteCef>()
                .Take(1)
                .SingleOrDefault();
        }
    }
}