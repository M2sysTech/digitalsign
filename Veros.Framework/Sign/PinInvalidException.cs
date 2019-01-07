//-----------------------------------------------------------------------
// <copyright file="PinInvalidException.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Sign
{
    using System;

    /// <summary>
    /// Exceção de pin inválido
    /// </summary>
    public class PinInvalidException : Exception 
    {
        /// <summary>
        /// Initializes a new instance of the PinInvalidException class
        /// </summary>
        /// <param name="message">Mensagem da exceção</param>
        public PinInvalidException(string message) : base(message)
        { 
        }
    }
}
