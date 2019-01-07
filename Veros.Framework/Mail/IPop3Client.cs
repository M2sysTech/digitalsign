//-----------------------------------------------------------------------
// <copyright file="IPop3Client.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Mail
{
    using System.Collections.Generic;

    /// <summary>
    /// Contrato para client pop3
    /// </summary>
    public interface IPop3Client
    {
        /// <summary>
        /// Retorna emails do servidor pop3
        /// </summary>
        /// <returns>Emails do servidor pop3</returns>
        IList<Email> GetEmails();
    }
}
