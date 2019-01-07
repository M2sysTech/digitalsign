//-----------------------------------------------------------------------
// <copyright file="Do.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework
{
    using System;

    public class Do
    {
        public static void WithTimeout(
            int timeoutMilliseconds,
            Action action)
        {
            WithTimeout(timeoutMilliseconds, true, action);
        }
        
        public static void WithTimeout(
            int timeoutMilliseconds, 
            bool throwExceptionWhenTimeout,
            Action action)
        {
            System.Threading.Thread threadToKill = null;

            Action wrappedAction = () =>
            {
                threadToKill = System.Threading.Thread.CurrentThread;
                action();
            };

            var result = wrappedAction.BeginInvoke(null, null);

            if (result.AsyncWaitHandle.WaitOne(timeoutMilliseconds))
            {
                wrappedAction.EndInvoke(result);
            }
            else
            {
                threadToKill.Abort();

                if (throwExceptionWhenTimeout)
                {
                    throw new TimeoutException();
                }
            }
        }
    }
}
