namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class ConfiguracaoDeLoteCefMap : ClassMap<ConfiguracaoDeLoteCef>
    {
        public ConfiguracaoDeLoteCefMap()
        {
            this.Table("CONFIGLOTECEF");
            this.Id(x => x.Id).Column("CONFIGLC_CODE").GeneratedBy.Native("SEQ_CONFIGLOTECEF");
            this.Map(x => x.HoraFechamento).Column("CONFIGLC_HORAFECHA");
            this.Map(x => x.QuantidadeMinima).Column("CONFIGLC_QTMIN");
            this.Map(x => x.QuantidadeMaxima).Column("CONFIGLC_QTMAX");
        }
    }
}
