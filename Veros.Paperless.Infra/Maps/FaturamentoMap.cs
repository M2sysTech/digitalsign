namespace Veros.Paperless.Infra.Maps
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class FaturamentoMap : ClassMap<Faturamento>
    {
        public FaturamentoMap()
        {
            this.Table("FATURAMENTO");
            this.Id(x => x.Id).Column("FATURAMENTO_CODE").GeneratedBy.Native("SEQ_FATURAMENTO");
            this.Map(x => x.DataFaturamento).Column("DT_FATURAMENTO");
            this.Map(x => x.EnvioFornecedor).Column("DT_RECEPCAOARQUIVO");
            this.Map(x => x.RecepcaoFornecedor).Column("DT_PROCESSSAMENTO");
            this.Map(x => x.TipoDeFaturamento).Column("TIPO_FATURAMENTO");
            this.Map(x => x.TotalContasRecepcionadas).Column("QTDCONTAS_RECEPCIONADAS");
            this.Map(x => x.ContasTratadasDentroDoSla2Horas).Column("QTD_SLAOK");
            this.Map(x => x.ContasTratadasForaDoSla).Column("QTDSLA_NOK");
            this.Map(x => x.ContasNaoTratadas).Column("QTDNAO_TRATADAS");
            this.Map(x => x.ContasTratadasComReconhecimentoAutomatico).Column("QTDRECONHECIMENTO_AUTO");
            this.Map(x => x.ContasTratadasComAnaliseManual).Column("QTDANALISE_MANUAL");
            this.Map(x => x.ContasTratadasComIndicativoDeAlteracaoCadastral).Column("QTDALTERACAO_CADASTRAL");
            this.Map(x => x.ContasTratadasComAtuacaoDoBanco).Column("QTDATUACAO_BANCO");
            this.Map(x => x.ContasLiberadasDiretamente).Column("QTDLIBERADAS");
            this.Map(x => x.ContasDevolvidas).Column("QTDDEVOLVIDAS");
            this.Map(x => x.ContasComIndicioDeFraude).Column("QTDAPONTAMENTO_FRAUDE");
            this.Map(x => x.Status).Column("FATURAMENTO_STATUS");
            this.Map(x => x.NomeArquivo).Column("NOME_ARQUIVO");
            this.Map(x => x.ContasTratadasDentroDoSla19Horas).Column("SLAOK19HORAS");
            this.Map(x => x.ContasTratadasDentroDoSlaApos17Horas).Column("SLAOKAPOS17HORAS");
        }
    }
}
