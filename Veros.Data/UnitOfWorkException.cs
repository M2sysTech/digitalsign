namespace Veros.Data
{
    using System;

    public class UnitOfWorkException : ApplicationException
    {
        public UnitOfWorkException(string message) : base(message)
        {
        }
    }
}
