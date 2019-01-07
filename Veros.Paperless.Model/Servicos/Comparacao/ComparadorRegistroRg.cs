namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using System;

    public class ComparadorRegistroRg : IComparador
    {
        public bool SaoIguais(string primeiroValor, string segundoValor)
        {
            if (string.IsNullOrEmpty(primeiroValor) ||
                 string.IsNullOrEmpty(segundoValor))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(primeiroValor) ||
                string.IsNullOrWhiteSpace(segundoValor))
            {
                return false;
            }

            if (this.ComparaValores(primeiroValor, segundoValor))
            {
                return true;
            }

            if (this.ComparaPorGrupo(primeiroValor, segundoValor))
            {
                return true;
            }

            return false;
        }

        public bool EhMenor(string primeiroValor, string segundoValor)
        {
            throw new NotImplementedException();
        }

        public bool EhMaior(string primeiroValor, string segundoValor)
        {
            throw new NotImplementedException();
        }

        public bool EhMenorOuIgual(string primeiroValor, string segundoValor)
        {
            throw new NotImplementedException();
        }

        public bool EhMaiorOuIgual(string primeiroValor, string segundoValor)
        {
            throw new NotImplementedException();
        }

        public bool Contem(string primeiroValor, string segundoValor)
        {
            throw new NotImplementedException();
        }

        private bool ComparaValores(string primeiroValor, string segundoValor)
        {
            if (primeiroValor.Contains("X") || segundoValor.Contains("X"))
            {
                return primeiroValor.ObterInteirosRegistroRg() == segundoValor.ObterInteirosRegistroRg();
            }

            return (primeiroValor.ObterInteirosRegistroRg() == segundoValor.ObterInteirosRegistroRg()) ||
            segundoValor.ObterInteirosRegistroRg().Contains(primeiroValor.ObterInteirosRegistroRg());
        }

        private bool ComparaPorGrupo(string primeiroValor, string segundoValor)
        {
            primeiroValor = primeiroValor.Replace('_', ' ');
            var grupos = primeiroValor.Split(' ');

            foreach (var grupo in grupos)
            {
                if (grupo == string.Empty)
                {
                    continue;
                }

                if (this.ComparaValores(grupo, segundoValor))
                {
                    return true;
                }
            }

            return false;
        }
    }
}