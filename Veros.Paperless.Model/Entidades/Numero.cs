namespace Veros.Paperless.Model.Entidades
{
    using System;

    public class Numero
    {
        public static string PorExtenso(int numero)
        {
            if (numero == 0)
            {
                return "zero";
            }

            if (numero < 0)
            {
                return "negativo " + PorExtenso(Math.Abs(numero));
            }

            var words = string.Empty;

            if ((numero / 1000000) > 0)
            {
                words += PorExtenso(numero / 1000000) + " milhoes ";
                numero %= 1000000;
            }

            if ((numero / 1000) > 0)
            {
                words += PorExtenso(numero / 1000) + " mil ";
                numero %= 1000;
            }

            if (numero > 0)
            {
                if (words != string.Empty)
                {
                    words += " e ";
                }

                var unidadesMap = new[] { "zero", "um", "dois", "três", "quatro", "cinco", "seis", "sete", "oito", "nove", "dez", "onze", "doze", "treze", "catorze", "quinze", "dezesseis", "dezessete", "dezoito", "dezenove" };
                var dezenasMap = new[] { "zero", "dez", "vinte", "trinta", "quarenta", "cinquenta", "sessenta", "setenta", "oitenta", "noventa" };
                var centenaMap = new[] { "cem", "cento", "duzentos", "trezentos", "quatrocentos", "quinhentos", "seissentos", "setecentos", "oitocentos", "novecentos" };

                if (numero < 20)
                {
                    words += unidadesMap[numero];
                }
                else if (numero >= 20 && numero < 100)
                {
                    words += dezenasMap[numero / 10];

                    if ((numero % 10) > 0)
                    {
                        words += " e " + unidadesMap[numero % 10];
                    }
                }
                else
                {
                    words += centenaMap[numero / 100];

                    if ((numero % 100) > 0)
                    {
                        words += " e " + PorExtenso(numero % 100);
                    }
                }
            }

            return words;
        }
    }
}