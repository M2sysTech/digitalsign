namespace Veros.Framework
{
    using System;

    public static class HumanizeExtensions
    {       
        public static string Humanize(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static string Humanize(this bool value)
        {
            return value ? "Sim" : "Não";
        }

        public static string Humanize(this decimal value)
        {
            return value.ToString("#,##0.00");
        }
    }
}
