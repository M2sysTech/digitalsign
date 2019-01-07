namespace Veros.Paperless.Model.Entidades
{
    public class LogDocumento : LogBase
    {
        //// TODO: Verificar código de ação coreto
        public static string AcaoDocumentoGenerico = "1";
        public static string AcaoDocumentoProvazero = "9";
        public static string AcaoDocumentoDigitacao = "2";
        public static string AcaoDocumentoAlteracaoValorFinal = "9";
        public static string AcaoDocumentoOcr = "OC";
        public static string AcaoDocumentoIdentificacao = "10";
        public static string AcaoAlteraNome = "AN";
        public static string AcaoAdicionaPagina = "PU";
        public static string AcaoFotoConferidaPeloClassifier = "FC";
        public static string AcaoDocumentoDigitalizado = "DI";
        public static string AcaoNovaVersao = "NV";
        public static string AcaoPaginaRemovida = "PR";
        public static string AcaoDocumentoExcluido = "DR";
        public static string AcaoNovaVersaoDePagina = "VP";
        public static string AcaoInclusaoDeDocumento = "ID";
        public static string AcaoCriadoNaClassificacao = "IC";
        public static string AcaoExcluidoNaClassificacao = "EC";
        public static string AcaoExcluidoNaSeparacao = "ES";
        public static string AcaoDocumentoReclassificado = "RE";
        public static string AcaoClassificacaoAprovadaNaFormalistica = "AF";
        public static string AcaoDocumentoReclassificadoNoAjuste = "RA";
        public static string AcaoDocumentoRecebidoNaClassificacao = "RC";
        public static string AcaoDocumentoNaoClassificacao = "NC";
        public static string AcaoPaginaRemovidaNoAjuste = "EP";
        public static string AcaoMarcadoComProblemaNaClassificacao = "MP";
        public static string AcaoMarcadoComProblemaNaQualidadeM2Ssys = "M2";
        public static string AcaoMarcadoComProblemaNaQualidadeCef = "MC";
        public static string AcaoAssinaturaDigital = "SG";
        public static string AcaoPaginaRessucitada = "PR";
        public static string AcaoDocumentoCriadoNaSeparacao = "DN";
        public static string AcaoMudancaDeOrdem = "MO";
        public static string AcaoDocumentoNaTriagem = "CT";
        public static string AcaoPortalSuporte = "PT";
        public static string AcaoDocumentoReclassificadoNaTriagem = "RT";
        public static string AcaoDocumentoReclassificadoQualidadeM2Sys = "RQ";
        public static string AcaoDocumentoReclassificadoPeloOcr = "OC";
        public static string AcaoInclusaoDeDocumentoUpload = "UP";
        public static string AcaoAlteracaoDeDocumentoUpload = "UU";
        public static string AcaoErroPosInbox = "EP";
        public static string AcaoDocumentoNaoIdentificacao = "NI";
        public static string AcaoDocumentoRecebidoNaIdentificacaoManual = "RI";
        public static string AcaoClassificacaoAprovadaNaIdentificacaoManual = "AI";
        public static string AcaoExcluidoNaIdentificacaoManual = "EI";

        public virtual Documento Documento { get; set; }
    }
}