namespace Veros.Framework.Modelo
{
    using System;

    /// <summary>
    /// Representa uma exceção de regra violada
    /// </summary>
    public class RegraDeNegocioException : Exception
    {
        public RegraDeNegocioException(string message)
            : base(message)
        {
        }

        public RegraDeNegocioException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }
    }
}