namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class RegraImportadaMap : ClassMap<RegraImportada>
    {
        public RegraImportadaMap()
        {
            this.Table("REGRAIMP");
            this.Id(x => x.Id).Column("REGRAIMP_CODE").GeneratedBy.Native("SEQ_REGRAIMP");
            this.Map(x => x.Vinculo).Column("REGRAIMP_VINCULO");
            this.Map(x => x.Status).Column("REGRAIMP_STATUS");
            this.References(x => x.Processo).Column("PROC_CODE");
            this.References(x => x.Documento).Column("MDOC_CODE");
        }
    }
}
