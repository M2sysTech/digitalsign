namespace Veros.Data
{
    using System.Data;
    using NHibernate;
    using Veros.Framework;

    public class UnitOfWorkTransactionFactory : IUnitOfWorkTransactionFactory
    {
        public IUnitOfWorkTransaction Create(IsolationLevel isolationLevel, FlushMode flushMode)
        {
            return IoC.Current.Resolve<IUnitOfWorkTransaction>()
                .Begin(isolationLevel, flushMode);
        }
    }
}
