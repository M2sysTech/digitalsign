//-----------------------------------------------------------------------
// <copyright file="StopWord.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Text
{
    using System.Collections.Generic;

    /// <summary>
    /// Contrato para palavras conectivas
    /// </summary>
    public class StopWord : IStopWord
    {
        /// <summary>
        /// Palavras conectivas
        /// </summary>
        private readonly IList<string> stopWords;

        /// <summary>
        /// Initializes a new instance of the StopWord class
        /// </summary>
        public StopWord()
        {
            this.stopWords = new List<string>
            {
                "ainda", 
                "antes",
                "ao",
                "aonde",
                "aos",
                "apos",
                "aquele",
                "aqueles",
                "as",
                "assim",
                "com",
                "como",
                "cuja",
                "cujas",
                "cujo",
                "cujos",
                "da",
                "das",
                "de",
                "dela",
                "delas",
                "dele",
                "deles",
                "desde",
                "deste",
                "destes",
                "dispoe",
                "dispoem",
                "diversa",
                "diversas",
                "diverso",
                "diversos",
                "do",
                "dos",
                "dr",
                "dra",
                "durante",
                "em",
                "embora",
                "entao",
                "entre",
                "essa",
                "essas",
                "esse",
                "esses",
                "esta",
                "estas",
                "este",
                "estes",
                "isso",
                "isto",
                "ja",
                "lhe",
                "lhes",
                "lo",
                "mas",
                "mediante",
                "mesma",
                "mesmas",
                "mesmo",
                "mesmos",
                "na",
                "nas",
                "nem",
                "nesse",
                "nesses",
                "neste",
                "nestes",
                "no",
                "nos",
                "os",
                "ou",
                "outra",
                "outras",
                "outro",
                "outros",
                "pela",
                "pelas",
                "pelo",
                "pelos",
                "perante",
                "pois",
                "por",
                "portanto",
                "quais",
                "quaisquer",
                "qual",
                "qualquer",
                "quando",
                "quanto",
                "quantos",
                "que",
                "quer",
                "seja",
                "sem",
                "sendo",
                "seu",
                "seus",
                "sobre",
                "sr",
                "sra",
                "sua",
                "suas",
                "tal",
                "tambem",
                "teu",
                "teus",
                "toda",
                "todas",
                "todo",
                "todos",
                "tua",
                "tudo",
                "um",
                "uma"
            };
        }

        /// <summary>
        /// Verifica se a palavra é conectiva
        /// </summary>
        /// <param name="word">Palavra a ser verificada</param>
        /// <returns>True se sim, False se não</returns>
        public bool Contains(string word)
        {
            return this.stopWords.Contains(word.ToLower());
        }
    }
}