//-----------------------------------------------------------------------
// <copyright file="LoginSessionException.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Sign
{
    using System;

    /// <summary>
    /// Exceção de login no cartão
    /// </summary>
    public class LoginSessionException : Exception 
    {
        /// <summary>
        /// Initializes a new instance of the LoginSessionException class
        /// </summary>
        /// <param name="message">Mensagem da exceção</param>
        public LoginSessionException(string message) : base(message)
        { 
        }
    }
}
