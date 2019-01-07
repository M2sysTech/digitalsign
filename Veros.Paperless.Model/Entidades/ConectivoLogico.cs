namespace Veros.Paperless.Model.Entidades
{
    using Framework;

    public class ConectivoLogico : EnumerationString<ConectivoLogico>
    {
        public static readonly ConectivoLogico E = new ConectivoLogico("E", "E");
        public static readonly ConectivoLogico Ou = new ConectivoLogico("OU", "OU");
        public static readonly ConectivoLogico Complexo = new ConectivoLogico("C", "C");

        public ConectivoLogico(string value, string displayName)
            : base(value, displayName)
        {
        }
    }
}
