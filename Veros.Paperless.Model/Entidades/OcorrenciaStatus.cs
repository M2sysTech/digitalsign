namespace Veros.Paperless.Model.Entidades
{
    using System.Collections.Generic;
    using Framework;

    public class OcorrenciaStatus : EnumerationString<OcorrenciaStatus>
    {
        public static OcorrenciaStatus Registrada = new OcorrenciaStatus("01", "Registrada");

        public static OcorrenciaStatus AguardandoCef = new OcorrenciaStatus("02", "Aguardando CEF");

        public static OcorrenciaStatus AguardandoM2 = new OcorrenciaStatus("20", "Aguardando M2Sys");

        public static OcorrenciaStatus Finalizada = new OcorrenciaStatus("99", "Finalizada");

        public static OcorrenciaStatus Excluida = new OcorrenciaStatus("*", "Excluída");

        public OcorrenciaStatus(string value, string displayName) : base(value, displayName)
        {
        }

        public static IList<OcorrenciaStatus> ListaDoBanco()
        {
            return new List<OcorrenciaStatus>
            {
                OcorrenciaStatus.Registrada,
                OcorrenciaStatus.AguardandoCef
            };
        }

        public static IList<OcorrenciaStatus> ListaDaM2()
        {
            return new List<OcorrenciaStatus>
            {
                OcorrenciaStatus.AguardandoM2
            };
        }
    }
}
