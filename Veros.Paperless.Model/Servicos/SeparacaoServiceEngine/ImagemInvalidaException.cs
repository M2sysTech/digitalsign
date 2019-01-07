namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System;

    public class ImagemInvalidaException : Exception
    {
        public ImagemInvalidaException(string message) : base(message)
        {
        }

        public ImagemInvalidaException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}