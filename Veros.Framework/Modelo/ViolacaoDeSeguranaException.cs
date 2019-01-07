namespace Veros.Framework.Modelo
{
    /// <summary>
    /// Representa uma exce��o de regra violada
    /// </summary>
    public class ViolacaoDeSeguranaException : RegraDeNegocioException
    {
        /// <summary>
        /// Initializes a new instance of the SecurityViolationException class
        /// </summary>
        /// <param name="message">Mensagem da seguran�a violada</param>
        public ViolacaoDeSeguranaException(string message)
            : base(message)
        {
        }
    }
}