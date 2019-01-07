namespace Veros.Paperless.Model.Entidades
{
    using Framework;

    public class OperadorMatematico : EnumerationString<OperadorMatematico>
    {
        public static OperadorMatematico Nenhum = new OperadorMatematico(string.Empty, "Nenhum");
        public static OperadorMatematico Adicao = new OperadorMatematico("+", "Adi��o");
        public static OperadorMatematico Subtracao = new OperadorMatematico("-", "Subtra��o");
        public static OperadorMatematico Multiplicacao = new OperadorMatematico("*", "Multiplica��o");

        public OperadorMatematico(string value, string displayName) : base(value, displayName)
        {
        }
    }
}