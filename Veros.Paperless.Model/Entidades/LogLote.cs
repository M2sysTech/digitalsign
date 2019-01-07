namespace Veros.Paperless.Model.Entidades
{
    public class LogLote : LogBase
    {
        //// favor manter em ordem alfabetica pelo value atribuido (caracteres entre aspas)
        public static string AcaoLoteCapturado = "CA";
        public static string AcaoCapturaFinalizada = "CF";
        public static string AcaoDossieAlterado = "DA";
        public static string AcaoDocumentoDuplicado = "DD";
        public static string AcaoConverterDocumentacaoGeral = "DG";
        public static string AcaoDocumentoObrigatorioNaoEncontrado = "DO";
        public static string AcaoLoteExcluidoNaCaptura = "EC";
        public static string AcaoTratamentoDeExcluidasDaTriagem = "ET";
        public static string AcaoLoteFaltaPaginaNaPac = "FP";
        public static string AcaoGerarTermos = "GT";
        public static string AcaoLoteDevolvidoNaAprovacao = "LD";
        public static string AcaoLoteLiberadoNaAprovacao = "LL";
        public static string AcaoAdicionaNaQualidadeCef = "MQ";
        public static string AcaoMudouTipoDeDossie = "MT";
        public static string AcaoProcessamentoOcrConcluido = "OC";
        public static string AcaoProcessamentoOcrIniciado = "OI";
        public static string AcaoProcessamentoErro = "PE";
        public static string AcaoProcessamentoIniciado = "PI";
        public static string AcaoProcessamentoConcluido = "PC";
        public static string AcaoPortalSuporte = "PT";
        public static string AcaoSetaQualidadeM2 = "QM";
        public static string AcaoSetaQualidadeCef = "QC";
        public static string AcaoRecebidoNaTriagem = "RT";
        public static string AcaoSeparacaoAutomaticaFim = "SF";
        public static string AcaoSeparacaoAutomaticaInicio = "SI";
        public static string AcaoSeparacaoRealizada = "SS";
        public static string AcaoSeparacaoAutomaticaErro = "SX";
        public static string AcaoAcessoNaTelaCaptura = "TA";
        public static string AcaoTriagemRealizada = "TR";
        public static string AcaoSeparacaoSubfaseDownload = "W1";
        public static string AcaoSeparacaoSubfaseBrancos = "W3";
        public static string AcaoSeparacaoSubfaseThumbnail = "W5";
        public static string AcaoSeparacaoSubfaseOrientacao = "W7";
        public static string AcaoSeparacaoSubfaseClassificacao = "W9";
        public static string AcaoPriorizarLote = "PL";
        public static string AcaoDespriorizarLote = "DL";
        public static string AcaoLotesAprovadosQualidadeM2SysSemMarcas = "Q1";
        public static string AcaoLotesAprovadosQualidadeM2Sys = "Q2";
        public static string AcaoLotesNaFaseDeFaturamento = "Q3";
        public static string AcaoLotecefAprovadonaQualicef = "H1";
        
        public virtual Lote Lote { get; set; }
    }
}