//-----------------------------------------------------------------------
// <copyright file="SmtpClient.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------
namespace Veros.Framework.Mail
{
    using System;
    using System.Net;
    using System.Net.Mail;

    /// <summary>
    /// Servidor smtp
    /// </summary>
    public class SmtpClient : ISmtpClient
    {
        /// <summary>
        /// Cliente Smtp
        /// </summary>
        private System.Net.Mail.SmtpClient smtpClient;

        public string Host
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public string User
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Envia email
        /// </summary>
        /// <param name="email">Email a ser enviado</param>
        public void SendEmail(Email email)
        {
            this.smtpClient = new System.Net.Mail.SmtpClient(this.Host, this.Port);
            this.smtpClient.Credentials = new NetworkCredential(this.User, this.Password);

            MailMessage mailMassage = ConvertEmailToMessage(email);

            try
            {
                Log.Framework.DebugFormat("Enviando email '{0}' para '{1}'", email.Subject, email.To);
                this.smtpClient.Send(mailMassage);
            }
            catch (SmtpFailedRecipientException smtpFailed)
            {
                throw new SendMailException("Não foi possível localizar destinatário: " + smtpFailed.Message);
            }
        }

        /// <summary>
        /// Convert Email para MailMessage
        /// </summary>
        /// <param name="email">Email a ser convertido</param>
        /// <returns>Retorna um MailMessage</returns>
        private static MailMessage ConvertEmailToMessage(Email email)
        {
            var mailMassage = new MailMessage(
                new MailAddress(email.From, email.FromName),
                new MailAddress(email.To));

            mailMassage.Subject = email.Subject;
            mailMassage.Body = email.Body;
            mailMassage.IsBodyHtml = true;
            mailMassage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMassage.BodyEncoding = System.Text.Encoding.UTF8;

            AdicionarAnexos(email, mailMassage);

            return mailMassage;
        }

        private static void AdicionarAnexos(Email email, MailMessage mailMassage)
        {
            foreach (var anexo in email.Attachments)
            {
                var item = new Attachment(anexo.OriginalFileName);
                mailMassage.Attachments.Add(item);
            }
        }
    }
}
