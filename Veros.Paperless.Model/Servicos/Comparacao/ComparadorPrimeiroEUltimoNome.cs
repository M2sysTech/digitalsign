namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework;

    public class ComparadorPrimeiroEUltimoNome : IComparador
    {
        public bool SaoIguais(string primeiroValor, string segundoValor)
        {
            if (this.ValoresSaoValidos(primeiroValor, segundoValor) == false)
            {
                return false;
            }

            if (segundoValor.Contains(Environment.NewLine) == false && primeiroValor.Contains(Environment.NewLine) == false)
            {
                return this.ComparaPrimeiroComSegundo(primeiroValor, segundoValor);
            }
            
            var linhas = segundoValor.Split(new[]
            {
                Environment.NewLine
            }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var linha in linhas)
            {
                var saoIguais = this.ComparaPrimeiroComSegundo(primeiroValor, linha);

                if (saoIguais)
                {
                    return true;
                }
            }

            return false;
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

        private bool ComparaPrimeiroComSegundo(string valorDoCadastro, string valorEncontradoOcr)
        {
            valorEncontradoOcr = Equivalencia.RetiraNumeraisInteligentemente(valorEncontradoOcr);

            if (string.IsNullOrEmpty(valorDoCadastro) || string.IsNullOrEmpty(valorEncontradoOcr))
            {
                return false;
            }

            valorDoCadastro = valorDoCadastro
                .RemoverDiacritico()
                .RemoveAcentuacao()
                .ToLower()
                .Trim();

            valorEncontradoOcr = valorEncontradoOcr
                .RemoverDiacritico()
                .RemoveAcentuacao()
                .RemoveCaracteresNaoAlfa(true)
                .ToLower()
                .Trim();

            var separador = new[]
            {
                Convert.ToString(' '),
                Environment.NewLine
            };

            var nomesPrimeiroValor = valorDoCadastro
                .Split(separador, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var posicao = valorEncontradoOcr.IndexOf(nomesPrimeiroValor[0]);

            if (posicao > 0)
            {
                valorEncontradoOcr = valorEncontradoOcr.Substring(posicao);
            }

            var nomesSegundoValor = valorEncontradoOcr
                .Split(separador, StringSplitOptions.RemoveEmptyEntries).ToList();

            Equivalencia.RemovePalavrasCurtasNoFinal(ref nomesSegundoValor);

            var nomesParaComparacaoPrimeiroValor = (List<string>) this.ObterPrimeiroEUltimoNome(nomesPrimeiroValor);
            var nomesParaComparacaoSegundoValor = (List<string>) this.ObterPrimeiroEUltimoNome(nomesSegundoValor);

            return Equivalencia.CompararSequencia(nomesParaComparacaoPrimeiroValor, nomesParaComparacaoSegundoValor);
        }

        private IList<string> ObterPrimeiroEUltimoNome(List<string> nomes)
        {
            if (nomes.Count == 0)
            {
                return new List<string>();
            }

            return new List<string>
            {
                nomes.First(),
                nomes.Last()
            };
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