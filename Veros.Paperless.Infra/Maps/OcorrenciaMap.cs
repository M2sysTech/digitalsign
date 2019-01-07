namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class OcorrenciaMap : ClassMap<Ocorrencia>
    {
        public OcorrenciaMap()
        {
            this.Table("OCORRENCIA");
            this.Id(x => x.Id).Column("OCORRENCIA_CODE").GeneratedBy.Native("SEQ_OCORRENCIA");
            this.Map(x => x.DataRegistro).Column("OCORRENCIA_DATAREGISTRO");
            this.Map(x => x.Observacao).Column("OCORRENCIA_OBSERVACAO");
            this.Map(x => x.Status).Column("OCORRENCIA_STATUS");
            this.Map(x => x.HoraInicio).Column("OCORRENCIA_STARTTIME");
            this.References(x => x.Tipo).Column("TIPOOCORRENCIA_CODE");
            this.References(x => x.Documento).Column("MDOC_CODE");
            this.References(x => x.DossieEsperado).Column("DOSSIEESPERADO_CODE");
            this.References(x => x.Lote).Column("BATCH_CODE");
            this.References(x => x.Pacote).Column("PACOTE_CODE");
            this.References(x => x.UsuarioRegistro).Column("USR_CODE");
            this.References(x => x.UsuarioResponsavel).Column("OCORRENCIA_USRRESP");
            this.Map(x => x.OcorrenciaPaiId).Column("OCORRENCIA_CODEPAI");
        }
    }
}
