namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class PalavraTipoMap : ClassMap<PalavraTipo>
    {
        public PalavraTipoMap()
        {
            this.Table("TYPEWORD");
            this.Id(x => x.Id).Column("TYPEWORD_CODE").GeneratedBy.Native("SEQ_TYPEWORD");
            this.Map(x => x.Texto).Column("TYPEWORD_TEXTO");
            this.Map(x => x.Categoria).Column("TYPEWORD_CATEGORIA");
            this.References(x => x.TipoDocumento).Column("TYPEDOC_ID");
        }
    }
}