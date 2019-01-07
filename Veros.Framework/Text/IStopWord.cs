//-----------------------------------------------------------------------
// <copyright file="IStopWord.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Text
{
    /// <summary>
    /// Contrato para palavras conectivas
    /// </summary>
    public interface IStopWord
    {
        /// <summary>
        /// Verifica se a palavra é conectiva
        /// </summary>
        /// <param name="word">Palavra a ser verificada</param>
        /// <returns>True se sim, False se não</returns>
        bool Contains(string word);
    }
}