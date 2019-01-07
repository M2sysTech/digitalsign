namespace Veros.Paperless.Model.Servicos.Campos
{
    using System.Collections.Generic;
    using System.Linq;
    using Comparacao;
    using Framework;
    using System;

    public static class Nome
    {
        public static string AbreviarExcetoPrimeiroEUltimo(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                return null;
            }

            var nomes = nome.Trim().Split(new[] { ' ' });

            var primeiroNome = nomes.First();
            var ultimoNome = nomes.Last();

            var nomesIntermediarios = nomes.Where(x => x != primeiroNome).Where(x => x != ultimoNome);
            var nomeMontado = new List<string>();

            nomeMontado.Add(primeiroNome);
            
            foreach (var nomeIntermediario in nomesIntermediarios)
            {
                if (NomeEhAbreviavel(nomeIntermediario))
                {
                    nomeMontado.Add(nomeIntermediario.Substring(0, 1));
                }
                else
                {
                    nomeMontado.Add(nomeIntermediario);
                }
            }

            nomeMontado.Add(ultimoNome);

            return MontaNomes(nomeMontado);
        }

        public static string RetornaNomeESobreNome(string nomeOriginal)
        {
            var nome = nomeOriginal.PosicionarNomeESobreNome();

            return nome.RemoverDiacritico().RemoveAcentuacao().Replace("<", " ").Trim();
        }

        public static bool EstaAbreviado(string nome)
        {
            if (nome.NaoTemConteudo())
            {
                return false;
            }

            var nomeSemSinais = nome
                .RemoverDiacritico()
                .RemoveCaracteresNaoAlfa(true)
                .ToLower()
                .Trim();

            var nomesPrimeiroValor = nomeSemSinais.Split(
                new[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            var existeAbreviadoNoPrimeiroNome = nomesPrimeiroValor.Any(x => x.Length == 1);

            return existeAbreviadoNoPrimeiroNome;
        }

        private static bool NomeEhAbreviavel(string s)
        {
            string[] stringPreposicoes = { "DA", "DE", "DO", "DAS", "DOS" };
            return s.Length > 2 && !stringPreposicoes.Contains(s.ToUpper());
        }

        private static string MontaNomes(IList<string> nomes)
        {
            var result = string.Empty;

            foreach (string t in nomes)
            {
                result += t + " ";
            }

            return result.Trim();
        }
    }
}