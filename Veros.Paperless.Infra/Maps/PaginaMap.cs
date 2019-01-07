namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class PaginaMap : ClassMap<Pagina>
    {
        public PaginaMap()
        {
            this.Table("DOC");
            this.Id(x => x.Id).Column("DOC_CODE").GeneratedBy.Native("SEQ_DOC");
            this.Map(x => x.AgenciaRemetente).Column("DOC_AGENCIAREMET");
            this.Map(x => x.ImagemVersoEmBranco).Column("DOC_BLANKPAGEV");
            this.Map(x => x.ImagemFrenteEmBranco).Column("DOC_BLANKPAGEF");
            this.Map(x => x.DataCriacao).Column("DOC_DATE");
            this.Map(x => x.NomeArquivoSemExtensao).Column("DOC_FILENAME");
            this.Map(x => x.TamanhoImagemFrente).Column("DOC_FILESIZEF");
            this.Map(x => x.ImagemFront).Column("DOC_IMGF");
            this.Map(x => x.AnaliseConteudoOcr).Column("DOC_IMGV");
            this.Map(x => x.Info).Column("DOC_INFO");
            this.Map(x => x.Ordem).Column("DOC_PAGMDOC");
            this.Map(x => x.Status).Column("DOC_STATUS");
            this.Map(x => x.TipoArquivo).Column("DOC_FILETYPE");
            this.Map(x => x.FimOcr).Column("DOC_FIMOCR");
            this.Map(x => x.TipoArquivoOriginal).Column("DOC_FILETYPEV");
            this.Map(x => x.PaginaImpostoRenda).Column("DOC_IMPOSTORENDA");
            this.Map(x => x.StatusFace).Column("DOC_STATUSFACE");
            this.Map(x => x.TimeFace).Column("DOC_TIMEFACE");
            this.Map(x => x.DocCodeReferencia).Column("DOC_PAGBATCH");
            this.Map(x => x.Versao).Column("DOC_VERSAO");
            this.Map(x => x.PaginaPaiId).Column("DOC_PAI");
            this.Map(x => x.DataCenter).Column("DOC_DTCENTERID");
            this.Map(x => x.Recapturado).Column("DOC_RECAPTURADO");
            this.Map(x => x.CloudOk).Column("DOC_COULDOK");
            this.Map(x => x.RemovidoFileTransferM2).Column("DOC_FTREMOVIDO");
            this.Map(x => x.DataCenterAntesCloud).Column("DOC_DTCENTEROLD");

            this.References(x => x.Lote).Column("BATCH_CODE");
            this.References(x => x.Documento).Column("MDOC_CODE");

            this.HasMany(x => x.ValoresReconhecidos)
                .KeyColumn("DOC_CODE")
                .Cascade.None()
                .Inverse();

            this.DynamicUpdate();
        }
    }
}