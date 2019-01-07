namespace Veros.Paperless.Infra.Maps
{
    using Data.Hibernate;
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class DocumentoMap : ClassMap<Documento>
    {
        public DocumentoMap()
        {
            this.Table("MDOC");
            this.Id(x => x.Id).Column("MDOC_CODE").GeneratedBy.Native("SEQ_MDOC");
            this.Map(x => x.Status).Column("MDOC_STATUS");
            this.Map(x => x.Templates).Column("MDOC_TEMPLATES");
            this.Map(x => x.IndicioDeFraude).Column("MDOC_INDICIOFRAUDE");
            this.Map(x => x.HoraInicio).Column("MDOC_STARTTIME");
            this.Map(x => x.StatusDeConsulta).Column("MDOC_STATUSCONSULTA").Not.Update();
            this.Map(x => x.Reclassificado).Column("MDOC_RECLASSIFICADO");
            this.Map(x => x.Cpf).Column("MDOC_CPF");
            this.Map(x => x.SequenciaTitular).Column("MDOC_SEQTIT");
            this.Map(x => x.Virtual).Column("MDOC_VIRTUAL");
            this.Map(x => x.StatusDeFraude).Column("MDOC_STATUSFRAUDE");
            this.Map(x => x.Marca).Column("MDOC_MARCA");

            this.Map(x => x.TipoDeArquivo).Column("MDOC_TPARQUIVO");
            this.Map(x => x.Versao).Column("MDOC_VERSAO");
            this.Map(x => x.DocumentoPaiId).Column("MDOC_PAI");
            this.Map(x => x.UsuarioResponsavelId).Column("MDOC_USRRESP");
            this.Map(x => x.Ordem).Column("MDOC_ORDEM");
            this.Map(x => x.QuantidadeDePaginas).Column("MDOC_QTDEPAG");
            this.Map(x => x.Recontado).Column("MDOC_RECONTADO");
            this.Map(x => x.RecognitionService).Column("MDOC_RECOGNITIONOK");
            this.Map(x => x.RecognitionEm).Column("MDOC_RECOGNITIONEM");
            this.Map(x => x.RecognitionInicioEm).Column("MDOC_STARTRECOGN");
            this.Map(x => x.RecognitionPosAjusteInicioEm).Column("MDOC_STARTPOSRECOGN");
            this.Map(x => x.RecognitionPosAjusteServiceFinalizado).Column("MDOC_RECOGNITIONPOSOK");
            this.Map(x => x.RecognitionPosAjusteTerminoEm).Column("MDOC_RECOGNITIONPOSEM");
            this.Map(x => x.ArquivoDigital).Column("MDOC_ARQUIVODIGITAL");

            this.References(x => x.TipoDocumento).Column("TYPEDOC_ID");
            this.References(x => x.TipoDocumentoOriginal).Column("TYPEDOC_IDORIGINAL");
            this.References(x => x.Processo).Column("PROC_CODE");
            this.References(x => x.Lote).Column("BATCH_CODE");

            //// TODO: nao deve ser cascade
            this.HasMany(x => x.Indexacao)
                .KeyColumn("MDOC_CODE")
                .Cascade.None()
                .Inverse();

            //// TODO: nao deve ser cascade
            this.HasMany(x => x.Paginas)
                .KeyColumn("MDOC_CODE")
                .Cascade.None()
                .Inverse();

            this.DynamicUpdate();
        }
    }
}
