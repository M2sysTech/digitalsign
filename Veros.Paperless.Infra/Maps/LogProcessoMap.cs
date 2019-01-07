namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class LogProcessoMap : ClassMap<LogProcesso>
    {
        public LogProcessoMap()
        {
            this.Table("LOGPROC");
            this.Id(x => x.Id).Column("LOGPROC_CODE").GeneratedBy.Native("SEQ_LOGPROC");
            this.Map(x => x.Acao).Column("LOGPROC_ACTION");
            this.Map(x => x.Observacao).Column("LOGPROC_OBS");
            this.Map(x => x.DataRegistro).Column("LOGPROC_DATE");
            this.References(x => x.Processo).Column("PROC_CODE");
            this.References(x => x.Usuario).Column("USR_CODE");
        }
    }
}
