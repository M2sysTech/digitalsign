namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using System;
    using Entidades;
    using Framework;

    public class ComparadorDeTextoCurto : IComparador
    {
        public bool SaoIguais(string primeiroValor, string segundoValor)
        {
            if (string.IsNullOrEmpty(primeiroValor) || string.IsNullOrEmpty(segundoValor))
            {
                return false;
            }

            if (this.CompareUf(primeiroValor, segundoValor))
            {
                return true;
            }

            var score = 0;

            primeiroValor = primeiroValor
                .RemoverDiacritico()
                .RemoveAcentuacao()
                .Replace("\n", " ")
                .Trim();

            segundoValor = segundoValor
                .RemoverDiacritico()
                .RemoveAcentuacao()
                .Replace("\n", " ")
                .Trim();

            if (this.CompareNumeros(primeiroValor, segundoValor))
            {
                return true;
            }

            var valueWords = primeiroValor.Split(Convert.ToChar(" "));
            
            foreach (var t in valueWords)
            {
                if (segundoValor.ToUpper().Contains(t.ToUpper()) || this.CompareAbreviation(segundoValor, primeiroValor))
                {
                    score++;
                }
            }

            bool result;

            if (valueWords.Length >= 3)
            {
                result = Math.Abs(valueWords.Length - score) <= 1;
            }
            else
            {
                result = Math.Abs(valueWords.Length - score) == 0;
            }

            return result;
        }

        public bool EhMenor(string primeiroValor, string segundoValor)
        {
            return false;
        }

        public bool EhMaior(string primeiroValor, string segundoValor)
        {
            return false;
        }

        public bool EhMenorOuIgual(string primeiroValor, string segundoValor)
        {
            return this.SaoIguais(primeiroValor, segundoValor);
        }

        public bool EhMaiorOuIgual(string primeiroValor, string segundoValor)
        {
            return this.SaoIguais(primeiroValor, segundoValor);
        }

        public bool Contem(string primeiroValor, string segundoValor)
        {
            throw new NotImplementedException();
        }

        private bool CompareUf(string primeiroValor, string segundoValor)
        {
            return new EstadosBrasil(primeiroValor.Trim().RemoverDiacritico().ToLower())
                .Contains(segundoValor.Trim().RemoverDiacritico().ToLower());
        }

        private bool CompareAbreviation(string primeiroValor, string segundoValor)
        {
            return new PalavrasAbreviadas(primeiroValor.Trim().RemoverDiacritico().ToLower())
                .Contains(segundoValor.Trim().RemoverDiacritico().ToLower());
        }

        private bool CompareNumeros(string primeiroValor, string segundoValor)
        {
            var valueWords = primeiroValor.Split(Convert.ToChar(" "));
            if (valueWords.Length != 1)
            {
                return false;
            }

            double numero;
            var numeral = double.TryParse(primeiroValor, out numero);
            if (!numeral)
            {
                return false;
            }

            if (segundoValor.ToUpper().Contains(numero.ToString()))
            {
                return true;
            }

            return false;
        }
    }
}
