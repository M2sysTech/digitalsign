namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using System;
    using Framework;

    public class ComparadorDeParecidos : IComparador
    {
        public bool SaoIguais(string primeiroValor, string segundoValor)
        {
            if (string.IsNullOrEmpty(primeiroValor) || string.IsNullOrWhiteSpace(primeiroValor))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(primeiroValor) || string.IsNullOrEmpty(segundoValor))
            {
                return false;
            }

            if (primeiroValor == segundoValor)
            {
                return false;
            }
            
            primeiroValor = primeiroValor
                .RemoverDiacritico()
                .RemoveStopWords()
                ////.RemoverPalavrasComDoisCaracteres()
                .RemoveAcentuacao()
                .Replace("\n", " ").Trim();

            segundoValor = segundoValor
                .RemoverDiacritico()
                .RemoveStopWords()
                ////.RemoverPalavrasComDoisCaracteres()
                .RemoveAcentuacao()
                .Replace("\n", " ").Trim();

            if (string.IsNullOrEmpty(primeiroValor) || string.IsNullOrWhiteSpace(primeiroValor))
            {
                return false;
            }

            if (primeiroValor == segundoValor)
            {
                return false;
            }

            if (this.QuantidadeDeDiferencas(primeiroValor, segundoValor) <= 1)
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
            throw new NotImplementedException();
        }

        private int QuantidadeDeDiferencas(string primeiroValor, string segundoValor)
        {
            var totalDeDiferencas = Math.Abs(primeiroValor.Length - segundoValor.Length);

            if (totalDeDiferencas > 1)
            {
                return totalDeDiferencas;
            }

            var totalDeLetras = primeiroValor.Length;

            if (segundoValor.Length < totalDeLetras)
            {
                totalDeLetras = segundoValor.Length;
            }

            for (var i = 0; i < totalDeLetras; i++)
            {
                if (primeiroValor.Substring(i, 1) != segundoValor.Substring(i, 1))
                {
                    totalDeDiferencas++;
                }

                if (totalDeDiferencas > 1)
                {
                    break;
                }
            }

            return totalDeDiferencas;
        }
    }
}
