//-----------------------------------------------------------------------
// <copyright file="TokenManagerNotFoundException.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Sign
{
    using System;

    /// <summary>
    /// Exceção gerenciador do token não localizado
    /// </summary>
    public class TokenManagerNotFoundException : Exception 
    {
        /// <summary>
        /// Initializes a new instance of the TokenManagerNotFoundException class
        /// </summary>
        /// <param name="message">Mensagem da exceção</param>
        public TokenManagerNotFoundException(string message) : base(message)
        { 
        }
    }
}