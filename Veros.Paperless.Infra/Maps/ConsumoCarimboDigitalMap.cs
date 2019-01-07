namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class ConsumoCarimboDigitalMap : ClassMap<ConsumoCarimboDigital>
    {
        public ConsumoCarimboDigitalMap()
        {
            this.Table("CARIMBOCONSUMIDO");

            this.Id(x => x.Id).Column("CARIMBOCONSUMIDO_CODE").GeneratedBy.Native("SEQ_CARIMBOCONSUMIDO");
            this.Map(x => x.AssinadoEm).Column("CARIMBOCONSUMIDO_EM");
            this.References(x => x.Documento).Column("MDOC_CODE");
            this.References(x => x.Lote).Column("BATCH_CODE");

            this.DynamicUpdate();
        }
    }
}