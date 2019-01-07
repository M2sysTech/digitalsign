//-----------------------------------------------------------------------
// <copyright file="Email.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------
namespace Veros.Framework.Mail
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Representa um email
    /// </summary>
    public class Email
    {
        /// <summary>
        /// Initializes a new instance of the Email class
        /// </summary>
        public Email()
        {
            this.Attachments = new List<EmailAttachment>();
        }

        /// <summary>
        /// Gets or sets o destinatário da mensagem
        /// </summary>
        public string To
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets o remetente da mensagem
        /// </summary>
        public string From
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets o assunto da mensagem
        /// </summary>
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets o corpo da mensagem
        /// </summary>
        public string Body
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets o corpo da mensagem
        /// </summary>
        public List<EmailAttachment> Attachments
        {
            get;
            set;
        }

        public Guid Id
        {
            get;
            set;
        }

        public string FromName
        {
            get;
            set;
        }

        /// <summary>
        /// Retorna string com dados do email
        /// </summary>
        /// <returns>String com dados do email</returns>
        public override string ToString()
        {
            return string.Format(
                "de {0} para {1} com assunto '{2}'",
                this.From,
                this.To,
                this.Subject);
        }
    }
}
