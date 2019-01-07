//-----------------------------------------------------------------------
// <copyright file="PinInvalidException.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informa��o Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Sign
{
    using System;

    /// <summary>
    /// Exce��o de pin inv�lido
    /// </summary>
    public class PinInvalidException : Exception 
    {
        /// <summary>
        /// Initializes a new instance of the PinInvalidException class
        /// </summary>
        /// <param name="message">Mensagem da exce��o</param>
        public PinInvalidException(string message) : base(message)
        { 
        }
    }
}
