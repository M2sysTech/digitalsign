//-----------------------------------------------------------------------
// <copyright file="SendMailException.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informa��o Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------
namespace Veros.Framework.Mail
{
    using System;

    /// <summary>
    /// Exce��o � disparada quando n�o � enviado o email
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
