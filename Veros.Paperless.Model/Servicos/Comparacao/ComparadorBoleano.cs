namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using FrameworkLocal;

    public class ComparadorBoleano : IComparador
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

            var primeiroValorBooleano = Bool.Converter(primeiroValor);
            var segundoValorBooleano = Bool.Converter(segundoValor);

            return primeiroValorBooleano.Equals(segundoValorBooleano);
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