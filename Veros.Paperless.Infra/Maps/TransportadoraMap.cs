namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class TransportadoraMap : ClassMap<Transportadora>
    {
        public TransportadoraMap()
        {
            this.Table("TRANSPORTADORA");
            this.Id(x => x.Id).Column("TRANSP_CODE").GeneratedBy.Native("SEQ_TRANSP");
            this.Map(x => x.Nome).Column("TRANSP_NOME");
            this.Map(x => x.Cnpj).Column("TRANSP_CNPJ");
            this.Map(x => x.Status).Column("TRANSP_STATUS");

            this.DynamicUpdate();
        }
    }
}
