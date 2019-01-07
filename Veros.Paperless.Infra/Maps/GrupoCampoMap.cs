namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class GrupoCampoMap : ClassMap<GrupoCampo>
    {
        public GrupoCampoMap()
        {
            this.Table("GRUPOCAMPO");
            this.Id(x => x.Id).Column("GRUPOCAMPO_CODE").GeneratedBy.Native("SEQ_GRUPOCAMPO");
            this.Map(x => x.Nome).Column("GRUPOCAMPO_NAME");
            this.Map(x => x.Ordem).Column("GRUPOCAMPO_ORDER");
        }
    }
}