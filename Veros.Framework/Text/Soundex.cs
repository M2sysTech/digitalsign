//-----------------------------------------------------------------------
// <copyright file="Soundex.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Text
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Soundex de palavra
    /// </summary>
    public class Soundex : ISoundex
    {
        /// <summary>
        /// Executa soundex de palavra
        /// </summary>
        /// <param name="word">Uma palavra</param>
        /// <returns>Soundex de palavra</returns>
        public string Execute(string word)
        {       
            return word
                .ToLower()
                .Replace('y', 'i')
                .Replace('w', 'f')
                .Replace("nh", "m")
                .Replace("ph", "f")
                .Replace("ss", "s")
                .Replace("ch", "xi")
                .Replace("çs", "ss")
                .Replace('b', 'p')
                .Replace('d', 't')
                .Replace('g', 'j')
                .Replace('k', 'c')
                .Replace('n', 'm')
                .Replace('q', 'c')
                .Replace('v', 'f')
                .Replace('z', 's')
                .RemoveNaoAlfa()
                .RemoveVogais(1)
                .Replace("x", "xi")
                .Replace("ç", "ss")
                .Replace("h", string.Empty);
        }
    }
}
