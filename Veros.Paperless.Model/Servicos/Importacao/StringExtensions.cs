namespace Veros.Paperless.Model.Servicos.Importacao
{
    public static class StringExtensions
    {
        public static string Right(this string svalue, int maxLength)
        {
            if (string.IsNullOrEmpty(svalue))
            {
                svalue = string.Empty;
            }
            else if (svalue.Length > maxLength)
            {
                svalue = svalue.Substring(svalue.Length - maxLength, maxLength);
            }

            return svalue;
        }
    }
}
