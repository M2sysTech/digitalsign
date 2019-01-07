namespace Veros.Paperless.Model.FrameworkLocal
{
    public static class Bool
    {
        public static bool Converter(string valor)
        {
            switch (valor.ToLower())
            {
                case "s":
                case "sim":
                case "1":
                case "true":
                    return true;
            }

            return false;
        }
    }
}