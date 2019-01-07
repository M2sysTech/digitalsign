namespace Veros.Paperless.Model.Entidades
{
    using Framework;

    public class RegraVioladaStatus : EnumerationString<RegraVioladaStatus>
    {
        public static readonly RegraVioladaStatus EmTratamento = new RegraVioladaStatus("T", "Em Tratamento");
        public static readonly RegraVioladaStatus Pendente = new RegraVioladaStatus("P", "Pendente");
        public static readonly RegraVioladaStatus Aprovada = new RegraVioladaStatus("A", "Aprovada");
        public static readonly RegraVioladaStatus Marcada = new RegraVioladaStatus("M", "Marcada");

        public RegraVioladaStatus(string value, string displayName) : base(value, displayName)
        {
        }
    }
}
