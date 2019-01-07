namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class LojaMap : ClassMap<Loja>
    {
        public LojaMap()
        {
            this.Table("PREST");
            this.Id(x => x.Id).Column("PREST_CODE").GeneratedBy.Native("SEQ_PREST");
            this.Map(x => x.Nome).Column("PREST_NAME");
        }
    }
}
