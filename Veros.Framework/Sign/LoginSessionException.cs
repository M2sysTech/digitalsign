//-----------------------------------------------------------------------
// <copyright file="LoginSessionException.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informa��o Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Sign
{
    using System;

    /// <summary>
    /// Exce��o de login no cart�o
    /// </summary>
    public class LoginSessionException : Exception 
    {
        /// <summary>
        /// Initializes a new instance of the LoginSessionException class
        /// </summary>
        /// <param name="message">Mensagem da exce��o</param>
        public LoginSessionException(string message) : base(message)
        { 
        }
    }
}
