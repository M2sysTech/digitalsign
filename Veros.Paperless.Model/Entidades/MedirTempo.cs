namespace Veros.Paperless.Model.Entidades
{
    using System;

    public class MedirTempo
    {
        public static TimeSpan De<T>(Func<T> func)
        {
            var inicio = DateTime.Now;

            func();

            var fim = DateTime.Now;

            return fim.Subtract(inicio);
        }

        public static TimeSpan De(Action action)
        {
            var inicio = DateTime.Now;

            action();

            var fim = DateTime.Now;

            return fim.Subtract(inicio);
        }
    }
}