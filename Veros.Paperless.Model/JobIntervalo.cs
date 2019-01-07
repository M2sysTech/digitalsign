namespace Veros.Paperless.Model
{
    using System;

    public class JobIntervalo
    {
        public static TimeSpan DeSegundos(int segundos)
        {
#if DEBUG
            return TimeSpan.FromSeconds(2);
#else
            return TimeSpan.FromSeconds(segundos);
#endif
        }

        public static TimeSpan DeMinutos(int minutos)
        {
#if DEBUG
            return TimeSpan.FromSeconds(2);
#else
            return TimeSpan.FromMinutes(minutos);
#endif
        }
    }
}