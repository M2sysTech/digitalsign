namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Framework.Text;

    public static class StringExtensions
    {
        public static string ObterInteiros(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var numbers = new List<char>("0123456789");
            var somenteNumeros = new StringBuilder(text.Length);
            var enumerator = text.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (numbers.Contains(enumerator.Current))
                {
                    somenteNumeros.Append(enumerator.Current);
                }
            }

            return somenteNumeros.ToString();
        }

        public static bool PossuiNumeros(this string text)
        {
            var numeros = text.ObterInteiros();

            if (string.IsNullOrEmpty(numeros))
            {
                return false;
            }

            return true;
        }

        public static bool PossuiLetras(this string text, int qtdMinimaDeLetras = 2)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            var letras = new List<char>("ABCDEFGHIJLMNOPQRSTUVXZYWK ");
            var qtdLetras = 0;

            for (var i = 0; i < text.Length; i++)
            {
                if (letras.Contains(Convert.ToChar(text.Substring(i, 1).ToUpper())))
                {
                    qtdLetras++;

                    if (qtdLetras >= qtdMinimaDeLetras)
                    {
                        return true;
                    }
                }
            }

            return qtdLetras >= qtdMinimaDeLetras;
        }

        public static bool PareceSerData(this string text)
        {
            var numerosDataData = text.ObterInteiros();

            if (numerosDataData.Length > 5 && numerosDataData.Length <= 8)
            {
                return true;
            }

            if (text.IndexOf("-") > -1 || text.IndexOf("/") > -1)
            {
                return text.FormatarData().Length > 2;
            }

            return false;
        }

        public static string RemoverDiacritico(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            var nomalizedText = text.Normalize(NormalizationForm.FormD);

            var sb = new StringBuilder();

            foreach (var character in nomalizedText)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);

                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(character);
                }
            }

            var result = sb.ToString();

            result = result.Replace(".", string.Empty);
            result = result.Replace("-", string.Empty);
            result = result.Replace("^", string.Empty);

            return result;
        }

        public static string RemoveStopWords(this string text)
        {
            var textToCompare = text.Split(Convert.ToChar(" "));

            var result = new StringBuilder();

            foreach (string t in textToCompare)
            {
                if (new StopWord().Contains(t) == false)
                {
                    result.Append(t + " ");
                }
            }

            return result.ToString();
        }

        public static string RemoverPalavrasComDoisCaracteres(this string text)
        {
            if (text.Length <= 2)
            {
                return text;
            }

            var textToCompare = text.Split(Convert.ToChar(" "));

            var result = new StringBuilder();

            foreach (string t in textToCompare)
            {
                if (t.Length > 2)
                {
                    result.Append(t + " ");
                }
            }

            return result.ToString();
        }
        
        public static string CorrigirMesDaData(this string data)
        {
            //// TODO: tirar de stringextension. Criar classe para normalizar string
            var meses = new Dictionary<string, string>
            {
                { "jum", "junho" },
                { "jumho", "junho" },
                { "jumh0", "junho" },
                { "ju1", "julho" },
                { "ju1ho", "julho" },
                { "ju1h0", "julho" },
                { "ag0", "agosto" }
            };

            foreach (var mes in meses.Where(mes => data.ToLower().Contains(mes.Key)).OrderByDescending(x => x.Key.Length))
            {
                return data.ToLower().Replace(mes.Key, mes.Value);
            }

            return data;
        }

        public static string RemoverEspacosEntreAsPalavras(this string text)
        {
            var textToCompare = text.Split(Convert.ToChar(" "));

            return textToCompare.Where(character => !character.Equals(string.Empty)).Aggregate(string.Empty,
                (current,
                    character) => current + character);
        }

        public static string PosicionarNomeESobreNome(this string nome)
        {
            int x;
            var primeiroNome = string.Empty;
            var sobreNome = string.Empty;

            for (x = 0; x <= nome.Length - 1; x++)
            {
                if (nome.Substring(x, 2) == "<<")
                {
                    sobreNome = nome.Substring(0, x);
                    primeiroNome = nome.Substring(x + 2, nome.IndexOf(nome.Substring(x + 3)));
                    break;
                }
            }

            for (x = 0; x <= primeiroNome.Length - 1; x++)
            {
                if (primeiroNome.Substring(x, 1) == "<")
                {
                    primeiroNome = string.Concat(primeiroNome.Replace("<", " ").Trim(), " ");
                    break;
                }
            }

            return string.Concat(primeiroNome, sobreNome);
        }

        public static string ObterInteirosRegistroRg(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var numbers = new List<char>("0123456789X");
            var somenteNumeros = new StringBuilder(text.Length);
            var enumerator = text.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (numbers.Contains(enumerator.Current))
                {
                    somenteNumeros.Append(enumerator.Current);
                }
            }

            return somenteNumeros.ToString();
        }
    }
}
