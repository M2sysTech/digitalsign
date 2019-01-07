namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class LogDossieEsperadoMap : ClassMap<LogDossieEsperado>
    {
        public LogDossieEsperadoMap()
        {
            this.Table("LOGDOSSIEESPERADO");
            this.Id(x => x.Id).Column("LOGDOSSIEESPERADO_CODE").GeneratedBy.Native("SEQ_LOGDOSSIEESPERADO");
            this.Map(x => x.Acao).Column("LOGDOSSIEESPERADO_ACTION");
            this.Map(x => x.DataRegistro).Column("LOGDOSSIEESPERADO_DATETIME");
            this.Map(x => x.Observacao).Column("LOGDOSSIEESPERADO_DESC");
            this.References(x => x.DossieEsperado).Column("DOSSIEESPERADO_CODE");
            this.References(x => x.Usuario).Column("USR_CODE");
        }
    }
}
