namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Framework;

    public class ComparadorNome : IComparador
    {
        public bool SaoIguais(string primeiroValor, string segundoValor)
        {
            if (this.ValoresSaoValidos(primeiroValor, segundoValor) == false)
            {
                return false;
            }

            segundoValor = Equivalencia.RetiraNumeraisInteligentemente(segundoValor);

            var nomesPrimeiroValor = primeiroValor
                .RemoverDiacritico()
                .RemoveAcentuacao()
                .Replace("\n", " ")
                .ToLower().Trim();

            var nomesSegundoValor = segundoValor
                .RemoverDiacritico()
                .RemoveAcentuacao()
                .RemoveCaracteresNaoAlfa(true)
                .Replace("\n", " ")
                .ToLower().Trim();

            var listaNomesPrimeiroValor = nomesPrimeiroValor.Split(' ').ToList();

            var posicao = nomesSegundoValor.IndexOf(listaNomesPrimeiroValor[0]);
            if (posicao > 0)
            {
                nomesSegundoValor = nomesSegundoValor.Substring(posicao);
            }

            List<string> listaNomesSegundoValor = nomesSegundoValor.Split(' ').ToList();
            Equivalencia.RemovePalavrasCurtasNoFinal(ref listaNomesSegundoValor);
            listaNomesPrimeiroValor.RemoveAll(x => x.Equals(string.Empty));
            listaNomesSegundoValor.RemoveAll(x => x.Equals(string.Empty));

            return Equivalencia.CompararSequencia(listaNomesPrimeiroValor, listaNomesSegundoValor);
        }

        public bool EhMenor(string primeiroValor, string segundoValor)
        {
            throw new System.NotImplementedException();
        }

        public bool EhMaior(string primeiroValor, string segundoValor)
        {
            throw new System.NotImplementedException();
        }

        public bool EhMenorOuIgual(string primeiroValor, string segundoValor)
        {
            throw new System.NotImplementedException();
        }

        public bool EhMaiorOuIgual(string primeiroValor, string segundoValor)
        {
            throw new System.NotImplementedException();
        }

        public bool Contem(string primeiroValor, string segundoValor)
        {
            throw new System.NotImplementedException();
        }

        private bool ValoresSaoValidos(string primeiroValor, string segundoValor)
        {
            if (string.IsNullOrWhiteSpace(primeiroValor) || string.IsNullOrWhiteSpace(segundoValor))
            {
                return false;
            }

            if (string.IsNullOrEmpty(primeiroValor) || string.IsNullOrEmpty(segundoValor))
            {
                return false;
            }

            return true;
        }
    }
}