namespace Veros.Data
{
    using System.Data;
    using NHibernate;

    public interface IUnitOfWorkTransactionFactory
    {
        IUnitOfWorkTransaction Create(IsolationLevel isolationLevel, FlushMode flushMode = FlushMode.Commit);
    }
}
