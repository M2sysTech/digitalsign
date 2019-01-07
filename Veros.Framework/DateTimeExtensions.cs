//-----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework
{
    using System;

    public static class DateTimeExtensions
    {       
        public static string Relative(this DateTime dateTime)
        {
            return dateTime.Relative(DateTime.Now);
        }
        
        public static string Relative(this DateTime dateTime, DateTime now)
        {
            const int Second = 1;
            const int Minute = 60 * Second;
            const int Hour = 60 * Minute;
            const int Day = 24 * Hour;
            const int Month = 30 * Day;

            var ts = now.Subtract(dateTime);
            var seconds = ts.TotalSeconds;

            if (seconds < 1 * Minute)
            {
                return ts.Seconds == 1 ? "há um segundo" : "há " + ts.Seconds + " segundos";
            }

            if (seconds == Minute)
            {
                return "há um minuto";
            }

            if (seconds < 60 * Minute)
            {
                return "há " + ts.Minutes + " minutos";
            }

            if (seconds < 120 * Minute)
            {
                return "há uma hora";
            }

            if (seconds < 24 * Hour)
            {
                return "há " + ts.Hours + " horas";
            }

            if (seconds < 48 * Hour)
            {
                return "ontem";
            }

            if (seconds < 30 * Day)
            {
                return "há " + ts.Days + " dias";
            }

            if (seconds < 12 * Month)
            {
                var months = System.Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "há um mês" : "há " + months + " meses";
            }

            var years = System.Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "há um ano" : "há " + years + " anos";
        }
    }
}
