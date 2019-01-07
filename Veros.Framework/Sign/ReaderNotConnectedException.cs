//-----------------------------------------------------------------------
// <copyright file="ReaderNotConnectedException.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Sign
{
    using System;

    /// <summary>
    /// Exceção de leitora não conectada
    /// </summary>
    public class ReaderNotConnectedException : Exception 
    {
        /// <summary>
        /// Initializes a new instance of the ReaderNotConnectedException class
        /// </summary>
        /// <param name="message">Mensagem da exceção</param>
        public ReaderNotConnectedException(string message) : base(message)
        { 
        }
    }
}
