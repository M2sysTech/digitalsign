namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class LogLoteMap : ClassMap<LogLote>
    {
        public LogLoteMap()
        {
            this.Table("LOGBATCH");
            this.Id(x => x.Id).Column("LOGBATCH_CODE").GeneratedBy.Native("SEQ_LOGBATCH");
            this.Map(x => x.Acao).Column("LOGBATCH_ACTION");
            this.Map(x => x.Observacao).Column("LOGBATCH_OBS");
            this.Map(x => x.DataRegistro).Column("LOGBATCH_DATE");
            this.References(x => x.Lote).Column("BATCH_CODE");
            this.References(x => x.Usuario).Column("USR_CODE");
        }
    }
}
