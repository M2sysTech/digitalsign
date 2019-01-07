namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class ConfiguracaoRemessaRepositorio : Repositorio<ConfiguracaoRemessa>, IConfiguracaoRemessaRepositorio
    {
        public ConfiguracaoRemessa ObterPorTipoProcesso(int tipoDeProcesso)
        {
            return this.Session.QueryOver<ConfiguracaoRemessa>()
                .Where(x => x.TipoDeProcesso == tipoDeProcesso)
                .Fetch(x => x.ConfiguracoesDosCampos).Eager
                .SingleOrDefault<ConfiguracaoRemessa>();
        }
    }
}
