namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class LogPaginaMap : ClassMap<LogPagina>
    {
        public LogPaginaMap()
        {
            this.Table("LOGDOC");
            this.Id(x => x.Id).Column("LOGDOC_CODE").GeneratedBy.Native("SEQ_LOGDOC");
            this.Map(x => x.Acao).Column("LOGDOC_ACTION");
            this.Map(x => x.Observacao).Column("LOGDOC_OBS");
            this.Map(x => x.DataRegistro).Column("LOGDOC_DATETIME");
            this.References(x => x.Pagina).Column("DOC_CODE");
            this.References(x => x.Usuario).Column("USR_CODE");
            this.References(x => x.Documento).Column("MDOC_CODE");
        }
    }
}
