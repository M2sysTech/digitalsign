namespace Veros.Paperless.Model.FrameworkLocal
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using Framework;

    public static class Extensions
    {
        public static int PaginateSkip(this int pagina, int itemsPorPagina)
        {
            return (pagina - 1) * itemsPorPagina;
        }

        public static bool EstaVazia(this object @object)
        {
            if (@object == null)
            {
                return true;
            }

            if (@object is string)
            {
                var @string = @object.ToString();
                return string.IsNullOrEmpty(@string.Trim());
            }

            return false;
        }

        //// TODO: mover pro framework
        public static bool ContemSomenteLetras(this string value)
        {
            return value.ToCharArray().All(char.IsLetter);
        }
    }
}
