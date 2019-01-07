namespace Veros.Paperless.Model.Entidades
{
    public class LogPagina : LogBase
    {
        public static string AcaoPaginaOcrIniciado = "OI";
        public static string AcaoPaginaOcrConcluido = "OC";
        public static string AcaoPaginaExtracaoLuxandOk = "LK";
        public static string AcaoPaginaExtracaoLuxandNok = "LX";
        public static string AcaoPaginaExcluida = "PE";
        public static string AcaoPaginaRessucitada = "PR";
        public static string AcaoFolhaExcluida = "FE";
        public static string AcaoFolhaRessucitada = "FR";
        public static string AcaoDocumentoCriadoNaSeparacao = "DN";
        public static string AcaoErroDownloadFileTransfer = "FT";
        public static string AcaoErroCopiarParaAbbyy = "CP";
        public static string AcaoMudarOrdem = "MO";
        public static string AcaoGiroTesseract = "GT";
        public static string AcaoGiroManual = "GM";
        public static string GiroRealizadoPeloDirectory = "GD";
        public static string ErroConsolidaAjuste = "EC";
        public static string AcaoPostadoNoCloud = "PC";
        public static string AcaoPaginaErroSeparador = "ES";
        public static string AcaoPaginaCorrompida = "CR";
        
        public virtual Pagina Pagina { get; set; }

        public virtual Documento Documento { get; set; }
    }
}