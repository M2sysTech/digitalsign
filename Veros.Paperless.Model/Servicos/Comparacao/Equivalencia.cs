namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Framework;

    public static class Equivalencia
    {
        //// Se os numeros 0,1,5,7 estiverem rodeados de letras, os troca por O,I,S,T respectivamente. 
        //// retira os demais numeros
        public static string RetiraNumeraisInteligentemente(string palavraAtual)
        {
            if (string.IsNullOrEmpty(palavraAtual))
            {
                return string.Empty;
            }

            string[] encontrar = new string[4] { "0", "1", "5", "7" };
            string[] trocarPor = new string[4] { "o", "i", "s", "t" };

            for (int i = 0; i < encontrar.Length; i++)
            {
                var posicao = palavraAtual.IndexOf(encontrar[i]);
                while (posicao >= 0)
                {
                    if (posicao >= 1 && posicao < palavraAtual.Length - 1)
                    {
                        var caractereAntes = palavraAtual.Substring(posicao - 1, 1);
                        var caractereDepois = palavraAtual.Substring(posicao + 1, 1);
                        if (Regex.IsMatch(caractereAntes, "[a-zA-Z]") && (Regex.IsMatch(caractereDepois, "[a-zA-Z]") || caractereDepois.Equals(" ")))
                        {
                            palavraAtual = palavraAtual.Substring(0, posicao) + trocarPor[i] + palavraAtual.Substring(posicao + 1);
                        }
                    }

                    posicao = palavraAtual.IndexOf(encontrar[i], posicao + 1);
                }
            }

            ////remove o resto dos numeros 
            palavraAtual = Regex.Replace(palavraAtual, "[0-9]", string.Empty);

            return palavraAtual;
        }

        public static void RemovePalavrasCurtasNoFinal(ref List<string> listaNomes)
        {
            for (int i = listaNomes.Count - 1; i >= 0; i--)
            {
                var palavraAtual = listaNomes[i];
                if (palavraAtual.Length > 2)
                {
                    break;
                }

                if (palavraAtual.Length == 1)
                {
                    listaNomes.RemoveAt(i);
                    continue;
                }

                if (ContemDuasVogaisOuDuasConsoantes(palavraAtual))
                {
                    listaNomes.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
        }

        public static bool ContemDuasVogaisOuDuasConsoantes(string palavraAtual)
        {
            return Regex.IsMatch(palavraAtual, "[b-df-hj-np-tv-zB-DF-HJ-NP-TV-Z]{2}$|[aeiouAEIOU]{2}$");
        }

        public static bool CompararSequencia(List<string> listaNomesPrimeiroValor, List<string> listaNomesSegundoValor)
        {
            if (listaNomesPrimeiroValor == null || listaNomesSegundoValor == null)
            {
                return false;
            }

            if (listaNomesPrimeiroValor.Count != listaNomesSegundoValor.Count)
            {
                return false;
            }

            var indexAtual = 0;
            foreach (var palavraAtual in listaNomesPrimeiroValor)
            {
                if (palavraAtual.Equals("de") || palavraAtual.Equals("da") || palavraAtual.Equals("do"))
                {
                    if (listaNomesSegundoValor[indexAtual].Length == 2)
                    {
                        if ("do".Contains(listaNomesSegundoValor[indexAtual].Substring(0, 1)) && listaNomesSegundoValor[indexAtual].Substring(1, 1) == palavraAtual.Substring(1, 1))
                        {
                            indexAtual++;
                            continue;
                        }
                    }
                }

                if (listaNomesSegundoValor[indexAtual].Equals(palavraAtual) == false)
                {
                    return false;
                }

                indexAtual++;
            }

            return true;
        }

        public static string LimpaConteudoNome(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return string.Empty;
            }

            texto = texto.RemoveAcentuacao()
                .RemoverDiacritico()
                .RemoveCaracteresNaoAlfa(true)
                .ToLower()
                .Trim();

            texto = Equivalencia.RetiraNumeraisInteligentemente(texto);
            if (string.IsNullOrEmpty(texto))
            {
                return string.Empty;
            }

            if (texto.Length <= 1)
            {
                return string.Empty;
            }

            return texto;
        }
    }
}
