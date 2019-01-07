namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using System;
    using System.Globalization;

    public static class DateTimeExtensions
    {
        public static string FormatarData(this string data)
        {
            DateTime dataFormatada;
            DateTime.TryParseExact(data, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFormatada);

            return dataFormatada == DateTime.MinValue ? string.Empty : dataFormatada.ToString("ddMMyyyy");
        }

        public static string FormatarDataIndexacao(this string data)
        {
            DateTime dataFormatada;
            DateTime.TryParseExact(data, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFormatada);

            if (dataFormatada == DateTime.MinValue)
            {
                DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFormatada);
            } 
            
            if (dataFormatada == DateTime.MinValue)
            {        
                DateTime.TryParseExact(data, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFormatada);
            }

            return dataFormatada == DateTime.MinValue ? string.Empty : dataFormatada.ToString("yyyy-MM-dd");
        }

        public static DateTime ConverteParaData(this string data)
        {
            DateTime dataFormatada;
            DateTime.TryParseExact(data, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFormatada);

            if (dataFormatada == DateTime.MinValue)
            {
                DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFormatada);
            }

            if (dataFormatada == DateTime.MinValue)
            {
                DateTime.TryParseExact(data, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFormatada);
            }

            return dataFormatada;
        }
    }
}
