namespace Veros.Paperless.Infra.Repositorios
{
    using NHibernate.Criterion;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class ConfiguracaoIpRepositorio :
        Repositorio<ConfiguracaoIp>, IConfiguracaoIpRepositorio
    {
        public ConfiguracaoIp ObterConfiguracaoDaFila()
        {
            return this.ObterPorTag("QUEUE");
        }

        public ConfiguracaoIp ObterConfiguracaoDaFilaDeOcr()
        {
            return this.ObterPorTag("QUEUEOCR");
        }

        public ConfiguracaoIp ObterConfiguracaoFileTransfer()
        {
            return this.Session.QueryOver<ConfiguracaoIp>()
                .Where(x => x.Tag == ConfiguracaoIp.TagFileTransfer)
                .SingleOrDefault<ConfiguracaoIp>();
        }

        public ConfiguracaoIp ObterConfiguracaoFileTransfer(int dataCenterId)
        {
            var query = this.Session.QueryOver<ConfiguracaoIp>()
                .Where(x => x.Tag == ConfiguracaoIp.TagFileTransfer + dataCenterId);
                ////.WhereRestrictionOn(x => x.Tag).IsLike(ConfiguracaoIp.TagFileTransfer + "%", MatchMode.Anywhere);
                
            if (dataCenterId > 0)
            {
                query.Where(x => x.DataCenterId == dataCenterId);
            }

            return query
                .SingleOrDefault<ConfiguracaoIp>();
        }

        public ConfiguracaoIp ObterPorTag(string tag)
        {
            return this.Session.QueryOver<ConfiguracaoIp>()
                .Where(x => x.Tag == tag)
                .Take(1)
                .SingleOrDefault<ConfiguracaoIp>();
        }
    }
}
