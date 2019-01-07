//-----------------------------------------------------------------------
// <copyright file="ExceptionExtension.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informa��o Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework
{
    using System;

    /// <summary>
    /// Extens�o da classe Exception
    /// </summary>
    public static class ExceptionExtension
    {
        /// <summary>
        /// Retorna detalhes da exce��o com os inners exceptions e a pilha
        /// </summary>
        /// <param name="exception">Exce��o a ser detalhada</param>
        /// <returns>Exce��o detalhada</returns>
        public static string GetAllExceptionsMessages(this Exception exception)
        {
            if (exception != null)
            {
                return string.Format(
                    "Exception: {0} \n Message: {1} \n Stack: {2} \n Inner Exception: {3} \n ",
                    exception.GetType(),
                    exception.Message,
                    exception.StackTrace,
                    GetAllExceptionsMessages(exception.InnerException));
            }
            
            return "null";
        }
    }
}
