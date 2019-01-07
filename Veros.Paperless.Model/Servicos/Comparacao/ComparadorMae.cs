namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using System;
    using System.Linq;
    using Campos;
    using Entidades;
    using Framework;
    using FrameworkLocal;

    public class ComparadorMae : IComparador
    {
        public bool SaoIguais(string primeiroValor, string segundoValor)
        {
            if (this.ValoresSaoValidos(primeiroValor, segundoValor) == false)
            {
                return false;
            }

            Log.Application.DebugFormat(
                "Comparação de nome da mãe [{0}] = [{1}]",
                primeiroValor,
                segundoValor);

            var primeiroValorSemSinais = primeiroValor
                .RemoverDiacritico()
                .RemoveCaracteresNaoAlfa(true)
                .ToLower()
                .Trim();

            var segundoValorSemSinais = segundoValor
                .RemoverDiacritico()
                .RemoveCaracteresNaoAlfa(true)
                .ToLower()
                .Trim();

            var primeiroValorFormatado = this.RetornaPrimeiroEUltimoNome(primeiroValorSemSinais);
            var segundoValorFormatado = this.RetornaPrimeiroEUltimoNome(segundoValorSemSinais);

            return primeiroValorFormatado == segundoValorFormatado;
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
            throw new NotImplementedException();
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

        private string RetornaPrimeiroEUltimoNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                return null;
            }

            var nomes = nome.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var primeiroNome = nomes.First();
            var ultimoNome = nomes.Last();

            return primeiroNome + " " + ultimoNome;
        }
    }
}