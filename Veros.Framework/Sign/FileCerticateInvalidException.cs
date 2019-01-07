//-----------------------------------------------------------------------
// <copyright file="FileCerticateInvalidException.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Sign
{
    using System;

    /// <summary>
    /// Exceção de arquivo de certificado invalido
    /// </summary>
    public class FileCerticateInvalidException : Exception 
    {
        /// <summary>
        /// Initializes a new instance of the FileCerticateInvalidException class
        /// </summary>
        /// <param name="message">Mensagem da exceção</param>
        public FileCerticateInvalidException(string message) : base(message)
        { 
        }
    }
}
