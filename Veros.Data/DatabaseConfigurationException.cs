//-----------------------------------------------------------------------
// <copyright file="DatabaseConfigurationException.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data
{
    using System;

    /// <summary>
    /// Exceção de configuração de banco de dados
    /// </summary>
    public class DatabaseConfigurationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the DatabaseConfigurationException class
        /// </summary>
        /// <param name="message">mensagem de erro</param>
        public DatabaseConfigurationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DatabaseConfigurationException class
        /// </summary>
        /// <param name="message">mensagem de erro</param>
        /// <param name="innerException">erro de origem</param>
        public DatabaseConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}