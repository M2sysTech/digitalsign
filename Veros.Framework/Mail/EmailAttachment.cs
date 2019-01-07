//-----------------------------------------------------------------------
// <copyright file="EmailAttachment.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------
namespace Veros.Framework.Mail
{
    /// <summary>
    /// Anexo de email
    /// </summary>
    public class EmailAttachment
    {
        /// <summary>
        /// Gets or sets nome do arquivo do anexo
        /// </summary>
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets nome do arquivo original que veio no email
        /// </summary>
        public string OriginalFileName
        {
            get;
            set;
        }
    }
}
