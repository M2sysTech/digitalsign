namespace Veros.Data
{
    using System;
    using System.Data;
    using NHibernate;

    public interface IUnitOfWorkTransaction : IDisposable
    {
        ISession CurrentSession
        {
            get;
        }

        void Commit();

        void RollBack();

        IUnitOfWorkTransaction Begin(
            IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted, 
            FlushMode flushMode = FlushMode.Commit);
    }
}
