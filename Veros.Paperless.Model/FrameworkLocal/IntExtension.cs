namespace Veros.Paperless.Model.FrameworkLocal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;

    public static class IntExtension
    {
        public static string PorExtenso(this int numero)
        {
            return Numero.PorExtenso(numero);
        }

        public static int ToInt(this string numero, int valorDefault)
        {
            int retorno;
            return int.TryParse(numero, out retorno) ? retorno : valorDefault;
        }

        public static IList<int> ToIntList(this string texto, char separador)
        {
            var lista = new List<int>();

            if (string.IsNullOrEmpty(texto))
            {
                return lista;
            }

            var itens = texto.Split(separador);

            foreach (var paginaId in itens.Where(x => string.IsNullOrEmpty(x) == false))
            {
                lista.Add(paginaId.ToInt(0));
            }

            return lista;
        }
    }
}