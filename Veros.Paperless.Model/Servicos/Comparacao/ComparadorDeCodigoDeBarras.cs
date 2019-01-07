namespace Veros.Paperless.Model.Servicos.Comparacao
{
    public class ComparadorDeCodigoDeBarras : IComparador
    {
        public bool SaoIguais(string primeiroValor, string segundoValor)
        {
            var auxiliar = string.Empty;
            var codigoDeBarras = string.Empty;

            if (primeiroValor.Length != segundoValor.Length)
            {
                return false;
            }

            if (primeiroValor.IndexOf('*') > 0 && segundoValor.IndexOf('*') > 0)
            {
                return false;
            }

            codigoDeBarras = primeiroValor.IndexOf('*') > 0 ? primeiroValor : segundoValor;

            for (var i = 0; i < codigoDeBarras.Length; i++)
            {
                if (codigoDeBarras.Substring(i, 1) == "*")
                {
                    auxiliar += "*";
                }
                else
                {
                    auxiliar += segundoValor.Substring(i, 1);
                }
            }

            return codigoDeBarras == auxiliar;
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
    }
}