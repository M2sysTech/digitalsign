namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using System;
    using System.Linq;
    using Entidades;
    using Framework;

    public class ComparadorDeTextos : IComparador
    {
        public bool SaoIguais(string primeiroValor, string segundoValor)
        {
            if (string.IsNullOrEmpty(primeiroValor) && string.IsNullOrEmpty(segundoValor))
            {
                return true;
            }

            if (string.IsNullOrEmpty(primeiroValor) || string.IsNullOrWhiteSpace(primeiroValor))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(segundoValor) || string.IsNullOrEmpty(segundoValor))
            {
                return false;
            }

            if (this.ComparaUf(primeiroValor, segundoValor))
            {
                return true;
            }

            if (primeiroValor == segundoValor)
            {
                return true;
            }

            primeiroValor = primeiroValor
                .RemoverDiacritico()
                .RemoveStopWords()
                .RemoverPalavrasComDoisCaracteres()
                .RemoveAcentuacao()
                .Replace("\n", " ").Trim();

            segundoValor = segundoValor
                .RemoverDiacritico()
                .RemoveStopWords()
                .RemoverPalavrasComDoisCaracteres()
                .RemoveAcentuacao()
                .Replace("\n", " ").Trim();

            if (string.IsNullOrEmpty(primeiroValor) || string.IsNullOrWhiteSpace(primeiroValor))
            {
                return false;
            }

            if (this.ComparaPorPalavras(primeiroValor, segundoValor))
            {
                return true;
            }

            return false;
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
            if (string.IsNullOrEmpty(primeiroValor) || string.IsNullOrEmpty(segundoValor))
            {
                return false;
            }

            var palavras = segundoValor.ToUpper().Split('¨');

            return palavras.Any(palavra => palavra.Equals(primeiroValor.ToUpper().Trim()));
        }

        private bool ComparaPorPalavras(string primeiroValor, string segundoValor)
        {
            var primeirasPalavras = primeiroValor.Split(Convert.ToChar(" "));
            var palavrasReconhecidas = 0;
            var valorFinal = string.Empty;

            foreach (var palavra in primeirasPalavras)
            {
                if (segundoValor.ToUpper().Contains(palavra.ToUpper()))
                {
                    palavrasReconhecidas++;
                    valorFinal += palavra.ToUpper() + " ";
                }
            }

            bool result;

            if (primeirasPalavras.Length >= 3)
            {
                result = Math.Abs(primeirasPalavras.Length - palavrasReconhecidas) <= 1;
            }
            else
            {
                result = Math.Abs(primeirasPalavras.Length - palavrasReconhecidas) == 0;
            }

            return result;
        }

        private bool ComparaUf(string firstValue, string secondeValue)
        {
            return new EstadosBrasil(firstValue.Trim().RemoverDiacritico().ToLower())
                .Contains(secondeValue.Trim().RemoverDiacritico().ToLower());
        }
    }
}
