namespace Veros.Paperless.Infra.Maps
{
    using Data.Hibernate;
    using FluentNHibernate.Mapping;
    using Veros.Paperless.Model.Entidades;

    public class CampoMap : ClassMap<Campo>
    {
        public CampoMap()
        {
            this.Table("TDCAMPOS");
            this.Id(x => x.Id).Column("TDCAMPOS_CODE").GeneratedBy.Native("SEQ_TDCAMPOS");
            this.Map(x => x.Description).Column("TDCAMPOS_DESC");
            this.Map(x => x.ReferenciaArquivo).Column("TDCAMPOS_REFARQUIVO");
            this.Map(x => x.ParaValidacao).Column("TDCAMPOS_VALIDACAO").BooleanoSimNao(); 
            this.Map(x => x.TipoDado).Column("TDCAMPOS_DATATYPE");
            this.Map(x => x.Ordem).Column("TDCAMPOS_ID");
            this.References(x => x.TipoDocumento).Column("TYPEDOC_ID");
            this.References(x => x.CampoParaBaterComPac).Column("TDCAMPOS_PAC");
            this.Map(x => x.Mascara).Column("TDCAMPOS_MASCARA");
            this.Map(x => x.MascaraComplemento).Column("TDCAMPOS_COMPLEMENTO");
            this.References(x => x.Grupo).Column("GRUPOCAMPO_CODE");
            this.Map(x => x.OrdemNoGrupo).Column("GRUPOCAMPO_ORDEM");
            this.Map(x => x.StyleNoGrupo).Column("GRUPOCAMPO_STYLE");
            this.Map(x => x.DuplaDigitacao).Column("TDCAMPOS_DUPLADIGIT").BooleanoSimNao();
            this.Map(x => x.Digitavel).Column("TDCAMPOS_DIGITAVEL").BooleanoSimNao();
            this.Map(x => x.Obrigatorio).Column("TDCAMPOS_OBRIGATORIO").BooleanoSimNao();
            this.Map(x => x.Indexador).Column("TDCAMPOS_INDEXADOR");
            this.Map(x => x.Reconhecivel).Column("TDCAMPOS_RECONHECIVEL");
            this.Map(x => x.PodeInserirPeloOcr).Column("TDCAMPOS_OCRPODEINSERIR");
            this.Map(x => x.TipoCampo).Column("TDCAMPOS_TIPO");
            
            //// TODO: nao deve ser cascade
            this.HasMany(x => x.MappedFields)
                .KeyColumn("TDCAMPOS_CODE")
                .Cascade.None()
                .Inverse();
        }
    }
}
