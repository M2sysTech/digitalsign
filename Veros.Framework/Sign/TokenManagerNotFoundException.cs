//-----------------------------------------------------------------------
// <copyright file="TokenManagerNotFoundException.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informa��o Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Sign
{
    using System;

    /// <summary>
    /// Exce��o gerenciador do token n�o localizado
    /// </summary>
    public class TokenManagerNotFoundException : Exception 
    {
        /// <summary>
        /// Initializes a new instance of the TokenManagerNotFoundException class
        /// </summary>
        /// <param name="message">Mensagem da exce��o</param>
        public TokenManagerNotFoundException(string message) : base(message)
        { 
        }
    }
}