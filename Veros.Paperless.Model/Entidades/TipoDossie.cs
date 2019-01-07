namespace Veros.Paperless.Model.Entidades
{
    using Veros.Framework;

    public class TipoDossie : EnumerationString<TipoDossie>
    {
        public static TipoDossie Fcvs = new TipoDossie("1", "FCVS");
        public static TipoDossie Cadmut = new TipoDossie("2", "CADMUT");

        public TipoDossie(string value, string displayName)
            : base(value, displayName)
        {
        }
    }
}
