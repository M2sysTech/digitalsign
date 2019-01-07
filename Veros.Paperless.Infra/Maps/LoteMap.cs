namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class LoteMap : ClassMap<Lote>
    {
        public LoteMap()
        {
            this.Table("BATCH");
            this.Id(x => x.Id).Column("BATCH_CODE").GeneratedBy.Native("SEQ_BATCH");
            this.Map(x => x.Agencia).Column("BATCH_AGENCIA");
            this.Map(x => x.Batido).Column("BATCH_BATIDO");
            this.Map(x => x.DataCriacao).Column("BATCH_DT");
            this.Map(x => x.Grupo).Column("BATCH_GROUP");
            this.Map(x => x.QuantidadeDocumentos).Column("BATCH_NUMDOCS");
            this.Map(x => x.PrioridadeNasFilas).Column("BATCH_QUEUEPRIOR");
            this.Map(x => x.Status).Column("BATCH_STATUS");
            this.Map(x => x.DataFimIcr).Column("BATCH_TICR");
            this.Map(x => x.DataFimIdentificacao).Column("BATCH_TIDENT");
            this.Map(x => x.DataFimDaMontagem).Column("BATCH_TMONTA");
            this.Map(x => x.DataFimDigitacao).Column("BATCH_TDIGIT");
            this.Map(x => x.DataFimConsulta).Column("BATCH_TCONSULT");
            this.Map(x => x.DataFimValidacao).Column("BATCH_TVALID");
            this.Map(x => x.DataFimProvaZero).Column("BATCH_TPRZ");
            this.Map(x => x.DataFimFormalistica).Column("BATCH_TFORMAL");
            this.Map(x => x.DataFimRevisao).Column("BATCH_TREVISAO");
            this.Map(x => x.DataAguardandoAprovacao).Column("BATCH_TAPROV");
            this.Map(x => x.DataFimArquivoXml).Column("BATCH_TFIMXML");
            this.Map(x => x.DataFimCaptura).Column("BATCH_TTX");
            this.Map(x => x.DataFimExportacao).Column("BATCH_TEXPORT");
            this.Map(x => x.DataFimEnvio).Column("BATCH_TENVIO");
            this.Map(x => x.DataFaturamento).Column("BATCH_TFATURAMENTO");
            this.Map(x => x.DataFimRetorno).Column("BATCH_TRETORNO");
            this.Map(x => x.DataFinalizacao).Column("BATCH_TFIM");
            this.Map(x => x.Identificacao).Column("BATCH_IDENTIFICACAO");
            this.Map(x => x.SequencialDeSolicitacao).Column("BATCH_SOLSEQ");
            this.Map(x => x.ConsultaPh3Realizada).Column("BATCH_PH3OK");
            this.Map(x => x.ConsultaVertrosRealizada).Column("BATCH_VERTROSOK");
            this.Map(x => x.QualidadeM2sys).Column("BATCH_QUALIM2");
            this.Map(x => x.QualidadeCef).Column("BATCH_QUALICEF");
            this.Map(x => x.ResultadoQualidadeCef).Column("BATCH_RESULTQCEF");
            this.Map(x => x.StatusAuxiliar).Column("BATCH_STAUX");
            this.Map(x => x.FoiGeradoTermoAvulso).Column("BATCH_TERMOPATCH");
            this.Map(x => x.AtualizacaoBrancoFinalizada).Column("BATCH_UPDBRANCO");
            this.Map(x => x.Hash).Column("BATCH_HASH");
            this.Map(x => x.Recapturado).Column("BATCH_RECAPTURADO");
            this.Map(x => x.ProblemaQualidade).Column("BATCH_TDC");
            this.Map(x => x.CloudOk).Column("BATCH_COULDOK");
            this.Map(x => x.PdfNoCloudEm).Column("BATCH_PDFCLOUDEM");
            this.Map(x => x.JpegNoCloudEm).Column("BATCH_JPGCLOUDEM");
            this.Map(x => x.RemovidoFileTransferEm).Column("BATCH_FTREMOVIDOEM");
            this.Map(x => x.JpegsEnviadosParaCloud).Column("BATCH_JPGNOCLOUD");
            this.Map(x => x.RemovidoFileTransferM2).Column("BATCH_FTREMOVIDO");

            this.References(x => x.Pacote).Column("PACK_CODE");
            this.References(x => x.PacoteProcessado).Column("PACOTEPROCESSADO_CODE");
            this.References(x => x.UsuarioCaptura).Column("USR_CAPTURA");
            this.References(x => x.DossieEsperado).Column("DOSSIEESPERADO_CODE");
            this.References(x => x.LoteCef).Column("LOTECEF_CODE");
            
            this.HasMany(x => x.Processos)
                .KeyColumn("BATCH_CODE")
                .Cascade.None()
                .Inverse();

            this.DynamicUpdate();
        }
    }
}
