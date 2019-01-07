namespace Veros.Paperless.Model.Servicos.Comparacao
{
    public class ComparadorDeNumeros : IComparador
    {
        public bool SaoIguais(string primeiroValor, string segundoValor)
        {
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
            var valor1 = this.CriarNumero(primeiroValor);
            var valor2 = this.CriarNumero(segundoValor);

            if (valor1 == null || valor2 == null)
            {
                return false;
            }

            return valor1 < valor2;
        }

        public bool EhMaior(string primeiroValor, string segundoValor)
        {
            var valor1 = this.CriarNumero(primeiroValor);
            var valor2 = this.CriarNumero(segundoValor);

            if (valor1 == null || valor2 == null)
            {
                return false;
            }

            return valor1 > valor2;
        }

        public bool EhMenorOuIgual(string primeiroValor, string segundoValor)
        {
            var valor1 = this.CriarNumero(primeiroValor);
            var valor2 = this.CriarNumero(segundoValor);

            if (valor1 == null || valor2 == null)
            {
                return false;
            }

            return valor1 <= valor2;
        }

        public bool EhMaiorOuIgual(string primeiroValor, string segundoValor)
        {
            var valor1 = this.CriarNumero(primeiroValor);
            var valor2 = this.CriarNumero(segundoValor);

            if (valor1 == null || valor2 == null)
            {
                return false;
            }

            return valor1 >= valor2;
        }

        public bool Contem(string primeiroValor, string segundoValor)
        {
            throw new System.NotImplementedException();
        }

        private bool ComparaValores(string primeiroValor, string segundoValor)
        {
            if (primeiroValor.ObterInteiros() == segundoValor.ObterInteiros())
            {
                return true;
            }

            var numerosPrimeiroValor = primeiroValor.ObterInteiros();
            if (string.IsNullOrEmpty(numerosPrimeiroValor) == false &&
                numerosPrimeiroValor.Length > 6 && 
                segundoValor.ObterInteiros().Contains(primeiroValor.ObterInteiros()))
            {
                return true;
            }

            return false;
        }

        private bool ComparaPorGrupo(string primeiroValor, string segundoValor)
        {
            primeiroValor = primeiroValor.Replace('_', ' ');
            var grupos = primeiroValor.Split(' ');

            foreach (var grupo in grupos)
            {
                if (this.ComparaValores(grupo, segundoValor))
                {
                    return true;
                }
            }

            return false;
        }

        private double? CriarNumero(string valor)
        {
            double numero;

            if (double.TryParse(valor, out numero))
            {
                return numero;
            }

            return null;
        }
    }
}
