//-----------------------------------------------------------------------
// <copyright file="TextTokenizer.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Text
{
    using System;
    using System.Linq;

    /// <summary>
    /// Tokenizer de texto
    /// </summary>
    public class TextTokenizer : ITextTokenizer
    {
        /// <summary>
        /// Executa o tokenizer de texto
        /// </summary>
        /// <param name="text">Texto para tokenizer</param>
        /// <returns>Palavras do texto</returns>
        public string[] Execute(string text)
        {
            var words = text
                .Replace('\n', ' ')
                .Replace('\r', ' ')
                .Replace('\t', ' ')
                .Replace(":", string.Empty)
                .Replace(",", string.Empty)
                .Replace(";", string.Empty)
                .Replace("(", string.Empty)
                .Replace(")", string.Empty)
                .Replace("[", string.Empty)
                .Replace("]", string.Empty)
                .Replace("/", string.Empty)
                .Replace(".", string.Empty)
                .Replace("-", string.Empty)
                .Replace("\"", string.Empty)
                .Replace("@", string.Empty)
                .Replace("~", string.Empty)
                .Replace("`", string.Empty)
                .Replace("_", string.Empty)
                .Replace("'", string.Empty)
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            const int MiniumLengthWord = 2;

            return words.Where(x => x.Length >= MiniumLengthWord).ToArray();
        }
    }
}