namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class TemplateMap : ClassMap<Template>
    {
        public TemplateMap()
        {
            this.Table("TYPEDOCTEMPLATE");
            this.Id(x => x.Id).Column("TEMPLATE_CODE").GeneratedBy.Native("SEQ_TYPEDOCTEMPLATE");
            this.Map(x => x.Nome).Column("TEMPLATE_NAME");
            this.Map(x => x.Chaves).Column("TEMPLATE_KEYS");
            this.Map(x => x.Ordem).Column("TEMPLATE_ORDER");
            this.References(x => x.TipoDocumento).Column("TYPEDOC_ID");
        }
    }
}
