//-----------------------------------------------------------------------
// <copyright file="ICryptography.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Security
{
    /// <summary>
    /// Contrato para classes que fazem criptografia
    /// </summary>
    public interface ICryptography
    {
        /// <summary>
        /// Realiza a criptografia em uma string
        /// </summary>
        /// <param name="value">String a ser criptografia</param>
        /// <returns>Resultado da criptografia</returns>
        string Encode(string value);

        /// <summary>
        /// Realiza a decriptografia em uma string
        /// </summary>
        /// <param name="value">String a ser decriptografada</param>
        /// <returns>Resultado da decriptografia</returns>
        string Decode(string value);
    }
}
