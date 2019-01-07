namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class OcorrenciaLogMap : ClassMap<OcorrenciaLog>
    {
        public OcorrenciaLogMap()
        {
            this.Table("LOGOCORRENCIA");
            this.Id(x => x.Id).Column("LOGOCORRENCIA_CODE").GeneratedBy.Native("SEQ_LOGOCORRENCIA");
            this.Map(x => x.DataRegistro).Column("LOGOCORRENCIA_DATAREGISTRO");
            this.Map(x => x.Observacao).Column("LOGOCORRENCIA_OBSERVACAO");
            this.References(x => x.Ocorrencia).Column("OCORRENCIA_CODE");
            this.References(x => x.UsuarioRegistro).Column("USR_CODE");
            this.Map(x => x.Acao).Column("LOGOCORRENCIA_ACAO");
        }
    }
}
