//-----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informa��o Ltda
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
                return ts.Seconds == 1 ? "h� um segundo" : "h� " + ts.Seconds + " segundos";
            }

            if (seconds == Minute)
            {
                return "h� um minuto";
            }

            if (seconds < 60 * Minute)
            {
                return "h� " + ts.Minutes + " minutos";
            }

            if (seconds < 120 * Minute)
            {
                return "h� uma hora";
            }

            if (seconds < 24 * Hour)
            {
                return "h� " + ts.Hours + " horas";
            }

            if (seconds < 48 * Hour)
            {
                return "ontem";
            }

            if (seconds < 30 * Day)
            {
                return "h� " + ts.Days + " dias";
            }

            if (seconds < 12 * Month)
            {
                var months = System.Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "h� um m�s" : "h� " + months + " meses";
            }

            var years = System.Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "h� um ano" : "h� " + years + " anos";
        }
    }
}
