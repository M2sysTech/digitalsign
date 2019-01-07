//-----------------------------------------------------------------------
// <copyright file="ITextTokenizer.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Text
{
    /// <summary>
    /// Contrato para tokenizer de texto
    /// </summary>
    public interface ITextTokenizer
    {
        /// <summary>
        /// Executa o tokenizer de texto
        /// </summary>
        /// <param name="text">Texto para tokenizer</param>
        /// <returns>Palavras do texto</returns>
        string[] Execute(string text);
    }
}