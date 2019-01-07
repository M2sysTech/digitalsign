namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class AberturaContaMap : ClassMap<AberturaConta>
    {
        public AberturaContaMap()
        {
            this.Table("ABERTURACONTA");
            this.Id(x => x.Id).Column("ABERTURACONTA_CODE").GeneratedBy.Native("SEQ_ABERTURACONTA");
            this.Map(x => x.Identificacao).Column("ABERTURACONTA_ID");
            this.Map(x => x.RecebidoEm).Column("ABERTURACONTA_RECEBIDOEM");
            this.Map(x => x.Status).Column("ABERTURACONTA_STATUS");
        }
    }
}
