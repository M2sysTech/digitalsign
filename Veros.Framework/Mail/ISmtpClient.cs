//-----------------------------------------------------------------------
// <copyright file="ISmtpClient.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------
namespace Veros.Framework.Mail
{
    /// <summary>
    /// Contrato com classe de smtpserver
    /// </summary>
    public interface ISmtpClient
    {
        string Host
        {
            get;
            set;
        }

        int Port
        {
            get;
            set;
        }

        string User
        {
            get;
            set;
        }

        string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Envia mensagens de e-mail a partir do servidor
        /// smtp
        /// </summary>
        /// <param name="email">Email a ser enviado</param>
        void SendEmail(Email email);
    }
}
