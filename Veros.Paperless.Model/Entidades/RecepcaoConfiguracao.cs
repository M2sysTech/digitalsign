namespace Veros.Paperless.Model.Entidades
{
    using System;

    public class RecepcaoConfiguracao
    {
        public static TimeSpan IntervaloDeEspera = TimeSpan.FromMinutes(2);
        public static decimal MaximoDeTentativas = 5;
    }
}