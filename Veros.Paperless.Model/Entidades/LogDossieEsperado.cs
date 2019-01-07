namespace Veros.Paperless.Model.Entidades
{
    public class LogDossieEsperado : LogBase
    {
        public static string AcaoAdicionarLogDossieNaCaixa = "ADC";
        public static string AcaoAdicionarLogOcorrenciaDossie = "AOD";
        public static string AcaoAdicionarLogMigrarDossie = "AMD";
        public static string AcaoAdicionarLogExcluirDossie = "AED";
        
        public virtual DossieEsperado DossieEsperado { get; set; }
    }
}