namespace Veros.Paperless.Model.Entidades
{
    using Framework;

    public class OrientacaoDePagina : EnumerationString<OrientacaoDePagina>
    {
        public static readonly OrientacaoDePagina Retrato = new OrientacaoDePagina("R", "Retrato");
        public static readonly OrientacaoDePagina Paisagem = new OrientacaoDePagina("P", "Paisagem");

        public OrientacaoDePagina(string value, string displayName) : base(value, displayName)
        {
        }
    }
}