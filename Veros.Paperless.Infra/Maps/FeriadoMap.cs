namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class FeriadoMap : ClassMap<Feriado>
    {
        public FeriadoMap()
        {
            this.Table("FERIADO");
            this.Id(x => x.Id).Column("FERIADO_CODE").GeneratedBy.Native("SEQ_FERIADO");
            this.Map(x => x.Data).Column("FERIADO_DATA");
            this.Map(x => x.Status).Column("FERIADO_ST");
        }
    }
}
