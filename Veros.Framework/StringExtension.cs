//-----------------------------------------------------------------------
// <copyright file="StringExtension.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Security;
    using System.Web.UI;

    /// <summary>
    /// Adicionais de funções para ser usado em strings
    /// </summary>
    public static class StringExtension
    {
        public static string ToPascalCase(this string value)
        {
            var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            return cultureInfo.TextInfo.ToTitleCase(value.ToLower());
        }

        /// <summary>
        /// Compara se duas string sao iguais. Se value for nulo
        /// não dispara NullPointerException, ao contrario de 
        /// string.Equals()
        /// </summary>
        /// <param name="value">valor a ser comparado</param>
        /// <param name="compareTo">valor para comparar</param>
        /// <returns>True se sao iguais</returns>
        public static bool SafeAreEquals(string value, string compareTo)
        {
            if (value == null)
            {
                return false;
            }

            return value.Equals(compareTo);
        }

        /// <summary>
        /// Converte codes para string de 9 posições preenchidos 
        /// com zero à esquerda 
        /// </summary>
        /// <param name="code">Código a ser formatado</param>
        /// <returns>Código Formatado</returns>
        public static string FormatCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return code;
            }

            return Convert.ToInt32(code).ToString("000000000");
        }

        public static bool IsWholeNumber(this string value)
        {
            var wholePattern = new Regex("[^0-9]");
            return !wholePattern.IsMatch(value);
        }

        /// <summary>
        /// Checa se último caracter da string é
        /// alfanumérico
        /// </summary>
        /// <param name="value">string a ser checada</param>
        /// <returns>true se último caracter é alfanumérico</returns>
        public static bool LastCharIsAlphaNumeric(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return new Regex("[A-Za-z0-9]").IsMatch(value.Substring(value.Length - 1));
        }

        /// <summary>
        /// Checa se um não é nulo e nem vazio
        /// </summary>
        /// <param name="value">objeto a ser validado</param>
        /// <returns>true se NÃO é nulo ou vazio</returns>
        public static bool IsNotNullOrEmpty(object value)
        {
            return value != null && value.ToString().Trim() != string.Empty;
        }

        /// <summary>
        /// Checa se string inicia com algum valor de uma lista
        /// </summary>
        public static bool ComecaCom(this string @string, params string[] values)
        {
            return values.Any(@string.StartsWith);
        }

        public static bool NaoContem(this string @string, params string[] values)
        {
            return values.Any(@string.Contains) == false;
        }

        /// <summary>
        /// Retorna ultimo caracter
        /// </summary>
        /// <param name="value">string a ser trabalhada</param>
        /// <returns>retorna o ultimo caracter</returns>
        public static string LastChar(string value)
        {
            return value.Substring(value.Length - 1, 1);
        }

        /// <summary>
        /// Remove o ultimo caracater
        /// </summary>
        /// <param name="value">string a ser trabalhada</param>
        /// <returns>retorna string sem o ultimo caracter</returns>
        public static string RemoveUltimoCaracter(this string value)
        {
            return value.Substring(0, value.Length - 1);
        }

        /// <summary>
        /// Remove o primeiro caracter
        /// </summary>
        /// <param name="value">string a ser trabalhada</param>
        /// <returns>retorna string sem o primeiro caracter</returns>
        public static string RemoveFirstChar(this string value)
        {
            return value.Remove(0, 1);
        }

        /// <summary>
        /// Remove o primeiro caracter
        /// </summary>
        /// <param name="value">string a ser trabalhada</param>
        /// <returns>retorna string sem o primeiro caracter</returns>
        public static bool Machinize(this string value)
        {
            return value == "Sim";
        }

        /// <summary>
        /// Gera uma string randomica de 5 caracteres
        /// </summary>
        /// <returns>String gerado randomicamente com 5 caracteres</returns>
        public static string GenerateRandom()
        {
           return StringExtension.GenerateRandom(5);
        }

        /// <summary>
        /// Gera uma string randomica
        /// </summary>
        /// <param name="lenght">Comprimento da string</param>
        /// <returns>String gerado randomicamente</returns>
        public static string GenerateRandom(int lenght)
        {
            var randomString = new StringBuilder();
            var randomNumber = new Random();

            for (int i = 0; i < lenght; ++i)
            {
                char appendedChar = Convert.ToChar(Convert.ToInt32(26 * randomNumber.NextDouble()) + 65);
                randomString.Append(appendedChar);
            }

            return randomString.ToString();
        }

        /// <summary>
        /// Converte string em array de bytes
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        /// <returns>Array de bytes</returns>
        public static byte[] ToBytes(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        /// <summary>
        /// Convert string para int 32. Caso passe nulo ou alfa, retorna 0
        /// </summary>
        /// <param name="value">String a ser convertida</param>
        /// <returns>Int 32. Caso passe nulo ou alfa, retorna 0</returns>
        public static int ToInt32Safe(this string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        
        /// <summary>
        /// Gera hash de um valor
        /// </summary>
        /// <param name="value">Valor a ser gerado hash</param>
        /// <returns>Hash do valor</returns>
        public static string Hash(this string value)
        {
            return new Hash().HashText(value);
        }

        public static string Left(this string value, int length)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            if (value.Length < length)
            {
                return value;
            }

            return value.Substring(0, length);
        }

        /// <summary>
        /// Remove vogais da string
        /// </summary>
        /// <param name="value">Valor da string</param>
        /// <param name="startIndex">Posição inicial</param>
        /// <returns>String sem vogais</returns>
        public static string RemoveVogais(this string value, int startIndex)
        {
            int position = startIndex;
            var vowels = new[]
            {
                'A', 'E', 'I', 'O', 'U', 'a', 'e', 'i', 'o', 'u', 'Á', 'É', 'Í', 'Ó', 'Ú', 'á', 'é',
                'í', 'ó', 'ú', 'à', 'è', 'ì', 'ò', 'ù', 'À', 'È', 'Ì', 'Ò', 'Ù', 'Ã', 'Õ', 'ã', 'õ',
                'ü', 'Ü', 'Â', 'Ê', 'Î', 'Ô', 'Û', 'â', 'ê', 'î', 'ô', 'û'
            };

            while (position != -1 && value.Length > 0)
            {
                position = value.IndexOfAny(vowels, position);
                if (position > 0)
                {
                    value = value.Remove(position, 1);
                }
            }

            return value;
        }

        /// <summary>
        /// Remove caracteres que não são alfa
        /// </summary>
        /// <param name="value">Valor da string</param>
        /// <returns>String com caracteres alfa</returns>
        public static string RemoveNaoAlfa(this string value)
        {
            string valueWithoutNonAlpha = string.Empty;
            var alpha = 
                "ABCDEFGHIJKLMNOPQRSTUVXZWYK" +
                "abcdefghijklmnopqrstuvxzwyk" +
                "ÁÉÍÓÚáéíóúàèìòùÀÈÌÒÙÃÕãõüÜÂÊÎÔÛâêîôûçÇ";

            for (int i = 0; i < value.Trim().Length; i++)
            {
                if (alpha.LastIndexOf(value[i]) > -1)
                {
                    valueWithoutNonAlpha += value[i];
                }
            }

            return valueWithoutNonAlpha;
        }

        /// <summary>
        /// Remove acentuação da string
        /// </summary>
        /// <param name="value">Valor da string</param>
        /// <returns>String sem acentuação</returns>
        public static string RemoveAcentuacao(this string value)
        {
            return value
                .Replace('à', 'a')
                .Replace('ã', 'a')
                .Replace('â', 'a')
                .Replace('á', 'a')
                .Replace('À', 'A')
                .Replace('Ã', 'A')
                .Replace('Â', 'A')
                .Replace('Á', 'A')
                .Replace('ç', 'c')
                .Replace('Ç', 'C')
                .Replace('é', 'e')
                .Replace('ê', 'e')
                .Replace('É', 'E')
                .Replace('Ê', 'E')
                .Replace('í', 'i')
                .Replace('Í', 'I')
                .Replace('ó', 'o')
                .Replace('õ', 'o')
                .Replace('ô', 'o')
                .Replace('Ó', 'O')
                .Replace('Õ', 'O')
                .Replace('Ô', 'O')
                .Replace('ú', 'u')
                .Replace('Ú', 'U')
                .Replace('ü', 'u')
                .Replace('Ü', 'U');
        }

        public static string RemoveLastCharIfIs(this string value, string compare)
        {
            value = value.Trim();

            if (value.Substring(value.Length - 1, 1).Equals(compare))
            {
                return value.Substring(0, value.Length - 1);
            }

            return value;
        }

        public static string[] SepararPor(this string value, string separador)
        {
            return value.SepararPor<string>(separador);
        }

        public static T[] SepararPor<T>(this string value, params string[] separadores)
        {
            return value.Split(separadores, StringSplitOptions.RemoveEmptyEntries).Select(x =>
            {
                if (typeof(int).IsAssignableFrom(typeof(T)))
                { 
                    return (T)(object)x.ToInt();
                }

                if (typeof(string).IsAssignableFrom(typeof(T)))
                {
                    return (T)(object) x.Trim();
                }
                
                throw new InvalidOperationException("Este tipo não está implementado.");
            }).ToArray();
        }

        public static string RemoveCaracteresEspeciais(this string self, string caracteresPermitidos = "")
        {
            if (self == null)
            {
                return null;
            }

            var normalizedString = self;

            var symbolTable = new Dictionary<string, string[]>
            {
                { "a", new[] { "à", "á", "ä", "â", "ã" } },
                { "c", new[] { "ç" } },
                { "e", new[] { "è", "é", "ë", "ê" } },
                { "i", new[] { "ì", "í", "ï", "î" } },
                { "o", new[] { "ò", "ó", "ö", "ô", "õ" } },
                { "u", new[] { "ù", "ú", "ü", "û" } }
            };

            foreach (var key in symbolTable.Keys)
            {
                foreach (var symbol in symbolTable[key])
                {
                    normalizedString = normalizedString.Replace(symbol, key);
                    normalizedString = normalizedString.Replace(symbol.ToUpper(), key.ToUpper());
                }
            }

            normalizedString = Regex.Replace(normalizedString, string.Format("[^0-9a-zA-Z {0}]+?", caracteresPermitidos), string.Empty);
            return normalizedString;
        }

        public static string RemoverCaracteresADireita(this string @string, int quantidadeDeCaracteresARemover)
        {
            //// TODO: testes
            //// TODO: checar nulo/vazio, quantidade maior que comprimento da string
            return @string.Substring(0, @string.Length - quantidadeDeCaracteresARemover);
        }

        public static string Right(this string @string, int length)
        {
            //// TODO: testes
            //// TODO: checar nulo/vazio, quantidade maior que comprimento da string
            return @string.Substring(@string.Length - length);
        }

        public static string RemoveCaracteresNaoAlfa(this string @string, bool preservaEspacos = false)
        {
            ////var rgx = new Regex("[^a-zA-Z0-9]");
            var rgx = new Regex(@"[^\w\s]*");
            return rgx.Replace(@string, string.Empty);
        }

        public static string With(this string input, params object[] args)
        {
            if (input == null)
            {
                return string.Empty;
            }

            return string.Format(input, args);
        }

        public static string With(this string format, object source)
        {
            return With(format, null, source);
        }

        private static string With(this string format, IFormatProvider provider, object source)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            var r = new Regex(
                @"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\})+",
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            var values = new List<object>();

            var rewrittenFormat = r.Replace(format, delegate(Match m)
            {
                var startGroup = m.Groups["start"];
                var propertyGroup = m.Groups["property"];
                var formatGroup = m.Groups["format"];
                var endGroup = m.Groups["end"];

                values.Add((propertyGroup.Value == "0")
                  ? source
                  : DataBinder.Eval(source, propertyGroup.Value));

                return new string('{', startGroup.Captures.Count) + (values.Count - 1) + formatGroup.Value + new string('}', endGroup.Captures.Count);
            });

            return string.Format(provider, rewrittenFormat, values.ToArray());
        }
    }
}