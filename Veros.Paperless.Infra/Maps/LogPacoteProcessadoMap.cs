namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class LogPacoteProcessadoMap : ClassMap<LogPacoteProcessado>
    {
        public LogPacoteProcessadoMap()
        {
            this.Table("LOGPACOTEPROCESSADO");
            this.Id(x => x.Id).Column("LOGPAC_CODE").GeneratedBy.Native("SEQ_LOGPACOTEPROCESSADO");
            this.References(x => x.Usuario).Column("USR_CODE");
            this.References(x => x.PacoteProcessado).Column("PACOTEPROCESSADO_CODE");
            this.Map(x => x.Acao).Column("LOGPAC_ACTION");
            this.Map(x => x.Observacao).Column("LOGPAC_OBS");
        }
    }
}
