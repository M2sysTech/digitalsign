namespace Veros.Paperless.Model.Servicos.AssinaturaDigital
{
    using System;

    public class RepositorioCertificadosException : Exception
    {
       public RepositorioCertificadosException(string message) : base(message)
        {
        }

       public RepositorioCertificadosException(string message, Exception innerException)
           : base(message, innerException)
        {
        }
    }
}