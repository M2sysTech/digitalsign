//-----------------------------------------------------------------------
// <copyright file="ReaderNotConnectedException.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informa��o Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Sign
{
    using System;

    /// <summary>
    /// Exce��o de leitora n�o conectada
    /// </summary>
    public class ReaderNotConnectedException : Exception 
    {
        /// <summary>
        /// Initializes a new instance of the ReaderNotConnectedException class
        /// </summary>
        /// <param name="message">Mensagem da exce��o</param>
        public ReaderNotConnectedException(string message) : base(message)
        { 
        }
    }
}
