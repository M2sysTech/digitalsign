namespace Veros.Paperless.Model.Entidades
{
    using Framework;

    public class ConectivoDeComparacao : EnumerationString<ConectivoDeComparacao>
    {
        public static readonly ConectivoDeComparacao Menor = new ConectivoDeComparacao("<", "Menor");
        public static readonly ConectivoDeComparacao Maior = new ConectivoDeComparacao(">", "Maior");
        public static readonly ConectivoDeComparacao Igual = new ConectivoDeComparacao("=", "Igual");
        public static readonly ConectivoDeComparacao MenorOuIgual = new ConectivoDeComparacao("<=", "Menor ou igual");
        public static readonly ConectivoDeComparacao MaiorOuIgual = new ConectivoDeComparacao(">=", "Maior ou igual");
        public static readonly ConectivoDeComparacao Diferente = new ConectivoDeComparacao("<>", "Diferente");
        public static readonly ConectivoDeComparacao In = new ConectivoDeComparacao("IN", "In");
        public static readonly ConectivoDeComparacao PrimeiroeUltimo = new ConectivoDeComparacao("~=", "Comparação primeiro e último");
        
        public ConectivoDeComparacao(string value, string displayName)
            : base(value, displayName)
        {
        }
    }
}
