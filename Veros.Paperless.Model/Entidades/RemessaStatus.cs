namespace Veros.Paperless.Model.Entidades
{
    using Framework;

    public class RemessaStatus : EnumerationString<RemessaStatus>
    {
        public static RemessaStatus SetaGeracao = new RemessaStatus("F0", "Seta Geracao");
        public static RemessaStatus ExportFinalizado = new RemessaStatus("FA", "Done Export");
        public static RemessaStatus StatusParaEnvio = new RemessaStatus("F5", "Para Envio");
        public static RemessaStatus RetornoFinalizado = new RemessaStatus("IA", "Done Retorno");
        public static RemessaStatus StatusFinalizada = new RemessaStatus("G0", "Finalizada");
        
        public RemessaStatus(string value, string displayName) : base(value, displayName)
        {
        }
    }
}
