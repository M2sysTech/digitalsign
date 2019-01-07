//-----------------------------------------------------------------------
// <copyright file="ISoundex.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Text
{
    /// <summary>
    /// Contrato para soundex de palavra
    /// </summary>
    public interface ISoundex
    {
        /// <summary>
        /// Executa soundex de palavra
        /// </summary>
        /// <param name="word">Uma palavra</param>
        /// <returns>Soundex de palavra</returns>
        string Execute(string word);
    }
}
