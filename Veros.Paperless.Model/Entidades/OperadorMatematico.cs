namespace Veros.Paperless.Model.Entidades
{
    using Framework;

    public class OperadorMatematico : EnumerationString<OperadorMatematico>
    {
        public static OperadorMatematico Nenhum = new OperadorMatematico(string.Empty, "Nenhum");
        public static OperadorMatematico Adicao = new OperadorMatematico("+", "Adição");
        public static OperadorMatematico Subtracao = new OperadorMatematico("-", "Subtração");
        public static OperadorMatematico Multiplicacao = new OperadorMatematico("*", "Multiplicação");

        public OperadorMatematico(string value, string displayName) : base(value, displayName)
        {
        }
    }
}