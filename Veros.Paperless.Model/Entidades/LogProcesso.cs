namespace Veros.Paperless.Model.Entidades
{
    public class LogProcesso : LogBase
    {
        //// TODO: Verificar código de ação coreto
        public static string AcaoDevolverNaAprovacao = "DA";
        public static string AcaoLiberarNaAprovacao = "LA";
        public static string AcaoEncerrarNaAprovacao = "EA";
        public static string AcaoAlteracaoDeCampo = "8";
        public static string AcaoRetornoDoPortal = "RP";
        public static string AcaoExportado = "EX";
        public static string AcaoRegrasExportadas = "RE";
        public static string AcaoNaoConsultaDocumentoscopia = "NC";
        public static string NaoFezClassifier = "SF";
        public static string AcaoComparacaoSelfieOk = "Lk";
        public static string AcaoControleQualidadeM2 = "QM";
        public static string AcaoControleQualidadeCef = "QC";
        public static string AcaoAjusteFinalizado = "FA";
        public static string AcaoSolicitaRecaptura = "SR";
        public static string AcaoSolicitaSeparacaoManual = "SM";
        public static string AcaoDocumentoMarcadoComProblema = "DP";
        public static string AcaoRetornarParaFluxo = "FL";
        public static string AcaoSaiuDaTriagem = "ST";
        public static string AcaoSolicitarProximoDossie = "SP";
        public static string AcaoAlterouDossie = "AD";
        public static string AcaoRetiradoDaFila = "R1";
        public static string AcaoRetornadoParaFila = "R0";
        public static string AcaoConsultaProcesso = "CP";

        public virtual Processo Processo { get; set; }
    }
}