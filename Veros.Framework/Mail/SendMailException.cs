//-----------------------------------------------------------------------
// <copyright file="SendMailException.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------
namespace Veros.Framework.Mail
{
    using System;

    /// <summary>
    /// Exceção é disparada quando não é enviado o email
    /// </summary>
    public class SendMailException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the SendMailException class
        /// </summary>
        /// <param name="message">Mensagem para ser construida</param>
        public SendMailException(string message)
            : base(message)
        {
        }
    }
}
