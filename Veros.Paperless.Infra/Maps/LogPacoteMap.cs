namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class LogPacoteMap : ClassMap<LogPacote>
    {
        public LogPacoteMap()
        {
            this.Table("LOGPACK");
            this.Id(x => x.Id).Column("LOGPACK_CODE").GeneratedBy.Native("SEQ_LOGPACK");
            this.Map(x => x.Acao).Column("LOGPACK_ACTION");
            this.Map(x => x.Observacao).Column("LOGPACK_OBS");
            this.Map(x => x.DataRegistro).Column("LOGPACK_DATETIME");
            this.References(x => x.Pacote).Column("PACK_CODE");
            this.References(x => x.Usuario).Column("USR_CODE");
        }
    }
}
