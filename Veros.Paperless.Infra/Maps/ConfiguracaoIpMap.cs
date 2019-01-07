namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class ConfiguracaoIpMap : ClassMap<ConfiguracaoIp>
    {
        public ConfiguracaoIpMap()
        {
            this.Table("IPCONFIG");
            this.Id(x => x.Id).Column("IPCONFIG_CODE").GeneratedBy.Native("SEQ_IPCONFIG");
            this.Map(x => x.Porta).Column("IPCONFIG_PORT");
            this.Map(x => x.Host).Column("IPCONFIG_IP");
            this.Map(x => x.Tag).Column("IPCONFIG_TAG");
            this.Map(x => x.DataCenterId).Column("IPCONFIG_DTCENTERID");
            this.Map(x => x.CaminhoChavePrivada).Column("IPCONFIG_PRIVATEKEY");
            this.Map(x => x.PassPhrase).Column("IPCONFIG_PASSPHRASE");
            this.Map(x => x.RemotePath).Column("IPCONFIG_REMOTEPATH");
            this.Map(x => x.SenhaSsh).Column("IPCONFIG_PWDSSH");
            this.Map(x => x.UsuarioSsh).Column("IPCONFIG_USRSSH");
            this.Map(x => x.UtilizaParDeChaves).Column("IPCONFIG_KEYPAIRS");
            this.Map(x => x.EnderecoWeb).Column("IPCONFIG_URLWEB");
        }
    }
}
