namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Model.Consultas;
    using Veros.Paperless.Model.Entidades;

    public class IndexacaoMap : ClassMap<Indexacao>
    {
        public IndexacaoMap()
        {
            this.ImportType<CampoDocumentoConsulta>();

            this.Table("MDOCDADOS");
            this.Id(x => x.Id).Column("MDOCDADOS_CODE").GeneratedBy.Native("SEQ_MDOCDADOS");
            this.Map(x => x.DataPrimeiraDigitacao).Column("MDOCDADOS_STARTTIME1").Nullable();
            this.Map(x => x.DataSegundaDigitacao).Column("MDOCDADOS_STARTTIME2");
            this.Map(x => x.ValorFinal).Column("MDOCDADOS_VALOROK");
            this.Map(x => x.PrimeiroValor).Column("MDOCDADOS_VALOR1");
            this.Map(x => x.SegundoValor).Column("MDOCDADOS_VALOR2");
            this.Map(x => x.UsuarioPrimeiroValor).Column("MDOCDADOS_USRDIGIT1");
            this.Map(x => x.UsuarioSegundoValor).Column("MDOCDADOS_USRDIGIT2");
            this.Map(x => x.ValorUtilizadoParaValorFinal).Column("MDOCDADOS_ELEITO");
            this.Map(x => x.OcrComplementou).Column("MDOCDADOS_OCRCOMPLEMENTOU");
            this.Map(x => x.ValidacaoComplementou).Column("MDOCDADOS_VALIDCOMPLEMENTOU");
            this.Map(x => x.ValorRecuperado).Column("MDOCDADOS_VALORRECUPERADO");
            this.References(x => x.Documento).Column("MDOC_CODE");
            this.References(x => x.Campo).Column("TDCAMPOS_CODE");

            this.DynamicUpdate();
        }
    }
}