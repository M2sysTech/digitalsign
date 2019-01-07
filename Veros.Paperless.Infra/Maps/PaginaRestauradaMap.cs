namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class PaginaRestauradaMap : ClassMap<PaginaRestaurada>
    {
        public PaginaRestauradaMap()
        {
            this.Table("PAGINARESTAURADA");
            this.Id(x => x.Id).Column("PAGINARESTAURADA_CODE").GeneratedBy.Native("SEQ_PAGINARESTAURADA");
            this.References(x => x.Documento).Column("MDOC_CODE");
            this.References(x => x.Pagina).Column("DOC_CODE");

            this.DynamicUpdate();
        }
    }
}