namespace Veros.Paperless.Model.Entidades
{
    using Framework;

    public class RegraImportadaStatus : EnumerationString<RegraImportadaStatus>
    {
        public static readonly RegraImportadaStatus Aprovada = new RegraImportadaStatus("A", "Aprovada");
        public static readonly RegraImportadaStatus Marcada = new RegraImportadaStatus("M", "Marcada");

        public RegraImportadaStatus(string value, string displayName) : base(value, displayName)
        {
        }
    }
}
