namespace Veros.Paperless.Model.FrameworkLocal
{
    using System;

    public static class DoubleExtension
    {
        public static double ConverterParaMoeda(this string texto)
        {
            double valor;

            var conseguiuConverter = double.TryParse(texto, out valor);

            valor = conseguiuConverter ? valor / 100 : 0.0;

            return valor;
        }
    }
}