namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class LogDocumentoMap : ClassMap<LogDocumento>
    {
        public LogDocumentoMap()
        {
            this.Table("LOGMDOC");
            this.Id(x => x.Id).Column("LOGMDOC_CODE").GeneratedBy.Native("SEQ_LOGMDOC");
            this.Map(x => x.Acao).Column("LOGMDOC_ACTION");
            this.Map(x => x.Observacao).Column("LOGMDOC_OBS");
            this.Map(x => x.DataRegistro).Column("LOGMDOC_DATE");
            this.References(x => x.Documento).Column("MDOC_CODE");
            this.References(x => x.Usuario).Column("USR_CODE");
        }
    }
}
