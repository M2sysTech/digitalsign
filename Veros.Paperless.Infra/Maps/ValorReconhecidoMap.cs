namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class ValorReconhecidoMap : ClassMap<ValorReconhecido>
    {
        public ValorReconhecidoMap()
        {
            this.Table("RECVALUE");
            this.Id(x => x.Id).Column("RECVALUE_CODE").GeneratedBy.Native("SEQ_RECVALUE");
            this.Map(x => x.Bottom).Column("RECVALUE_BOTTOM");
            this.Map(x => x.DataType).Column("RECVALUE_TYPE");
            this.Map(x => x.CampoTemplate).Column("RECVALUE_FIELD");
            this.Map(x => x.Height).Column("RECVALUE_HEIGHT");
            this.Map(x => x.Right).Column("RECVALUE_RIGHT");
            this.Map(x => x.TemplateName).Column("RECVALUE_TEMPLATE");
            this.Map(x => x.Top).Column("RECVALUE_TOP");
            this.Map(x => x.Value).Column("RECVALUE_VALUE");
            this.Map(x => x.Width).Column("RECVALUE_WIDTH");
            this.Map(x => x.Left).Column("RECVALUE_LEFT");
            this.References(x => x.Campo).Column("TDCAMPOS_CODE");
            this.References(x => x.Pagina).Column("DOC_CODE");
        }
    }
}