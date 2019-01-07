namespace Veros.Paperless.Model.Entidades
{
    using Framework;

    public class FaceStatus : EnumerationString<FaceStatus>
    {
        public static FaceStatus PendenteComparacao = new FaceStatus("P", "Pendente de Comparacao");
        public static FaceStatus ComparacaoFinalizada = new FaceStatus("F", "Comparacao Finalizada");

        public FaceStatus(string value, string counterName) : 
            base(value, counterName)
        {
        }
    }
}