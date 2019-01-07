namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System;
    using Comparacao;
    using Veros.Framework;

    public class ConversorLinhaDigitavel
    {
        private readonly string linhaDigitavel;

        public ConversorLinhaDigitavel(string linhaDigitavel)
        {
            this.linhaDigitavel = linhaDigitavel;
        }

        public string RetornaLinhaDigitavelEValida()
        {
            //// TODO: levar rotina para o framework
            var numero = this.linhaDigitavel.RemoverDiacritico().RemoveAcentuacao().Replace(" ", string.Empty).Trim();

            if (numero.Length != 47)
            {
                return string.Empty;
            }

            var anteriorDv1 = numero.Substring(9, 1);
            var anteriorDv2 = numero.Substring(20, 1);
            var anteriorDv3 = numero.Substring(31, 1);
            var anteriorDv4 = numero.Substring(32, 1);
            
            var codigoDeBarras = string.Concat(numero.Substring(0, 9), numero.Substring(10, 10), numero.Substring(21, 11), numero.Substring(33, 14));

            var num1 = codigoDeBarras.Substring(0, 9);
            var num2 = codigoDeBarras.Substring(9, 10);
            var num3 = codigoDeBarras.Substring(19, 10);
            var num4 = codigoDeBarras.Substring(29, 14);

            var codigoDeBarrasSemDv = string.Concat(numero.Substring(0, 4), numero.Substring(33, 4), numero.Substring(37, 10), 
                                                    numero.Substring(4, 5), numero.Substring(10, 10), numero.Substring(21, 10));

            var dv1 = this.CalculaDv(num1);
            if (anteriorDv1 != dv1)
            {
                return string.Empty;
            }    

            var dv2 = this.CalculaDv(num2);
            if (anteriorDv2 != dv2)
            {
                return string.Empty;
            }

            var dv3 = this.CalculaDv(num3);
            if (anteriorDv3 != dv3)
            {
                return string.Empty;
            }

            var dv4 = this.CalculaDvBc(codigoDeBarrasSemDv);
            if (anteriorDv4 != dv4)
            {
                return string.Empty;
            }

            return numero;
        }

        private int SomarDigito(string numero)
        {
            int x;
            var resultado = 0;

            for (x = 0; x <= numero.Length - 1; x++)
            {
                resultado += numero.Substring(x, 1).ToInt();
            }

            return resultado;
        }

        private string CalculaDv(string numero)
        {
            int i, peso = 1, somatorio = 0;
            int multiplo;

            for (i = numero.Length - 1; i >= 0; i--)
            {
                var @char = Convert.ToChar(numero.Substring(i, 1));
                if (char.IsDigit(@char) == false)
                {
                    return string.Empty;
                }

                peso = peso == 1 ? 2 : 1;

                var calculoNumero = numero.Substring(i, 1).ToInt() * peso;

                if (calculoNumero > 9)
                {
                    calculoNumero = this.SomarDigito(calculoNumero.ToString());
                }

                somatorio += calculoNumero;
            }

            var resultado = somatorio % 10;

            if (resultado != 0)
            {
                multiplo = 10 - resultado;
            }
            else
            {
                multiplo = resultado;
            }

            return multiplo.ToString();
        }

        private string CalculaDvBc(string numero)
        {
            int i, peso = 2, somatorio = 0;
            int multiplo;

            for (i = numero.Length - 1; i >= 0; i--)
            {
                var calculoNumero = numero.Substring(i, 1).ToInt() * peso;
                peso += 1;

                if (peso > 9)
                {
                    peso = 2;
                }

                somatorio += calculoNumero;
            }

            var resultado = somatorio % 11;

            if (resultado == 0 || resultado == 1)
            {
                multiplo = 1;
            }
            else
            {
                multiplo = 11 - resultado;
            }

            return multiplo.ToString();
        }
    }
}