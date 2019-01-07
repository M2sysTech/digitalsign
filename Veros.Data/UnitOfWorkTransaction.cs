namespace Veros.Data
{
    using System.Data;
    using Hibernate;
    using Veros.Framework;
    using NHibernate;

    public class UnitOfWorkTransaction : IUnitOfWorkTransaction
    {
        private readonly IUnitOfWorkStore unitOfWorkStore;
        private readonly ISessionBuilder sessionBuilder;
        private ISession session;
        private ITransaction transaction;

        public UnitOfWorkTransaction(IUnitOfWorkStore unitOfWorkStore, ISessionBuilder sessionBuilder)
        {
            this.unitOfWorkStore = unitOfWorkStore;
            this.sessionBuilder = sessionBuilder;
        }

        public ISession CurrentSession
        {
            get
            {
                return this.session;
            }
        }

        private bool ThereIsATransactionInProgress
        {
            get
            {
                return this.transaction != null && this.transaction.IsActive;
            }
        }

        public void Commit()
        {
            if (this.ThereIsATransactionInProgress == false)
            {
                throw new UnitOfWorkException("Must call Begin() on the unit of work before committing");
            }

            try
            {
                this.transaction.Commit();
                Log.Application.DebugFormat("Commit trans {0}", this.session.GetHashCode());
            }
            catch
            {
                Log.Application.ErrorFormat("Erro ao dar commit na trans {0}", this.session.GetHashCode());
                this.RollBack();
                throw;
            }
        }

        public void RollBack()
        {
            if (this.transaction.IsActive)
            {
                this.transaction.Rollback();
                Log.Application.DebugFormat("Rollback trans {0}", this.session.GetHashCode());
            }
        }

        public void Dispose()
        {
            try
            {
                if (this.ThereIsATransactionInProgress)
                {
                    this.Commit();
                }
            }
            finally
            {
                ////Log.Application.DebugFormat("Sessão {0} destruida", this.session.GetHashCode());

                this.transaction.Dispose();
                this.session.Dispose();

                this.unitOfWorkStore.Clear();
            }
        }

        public IUnitOfWorkTransaction Begin(
            IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted, 
            FlushMode flushMode = FlushMode.Commit)
        {
            if (this.ThereIsATransactionInProgress)
            {
                this.transaction.Dispose();
            }

            this.session = this.sessionBuilder.GetSession();
            this.session.FlushMode = flushMode;
            this.transaction = this.session.BeginTransaction(isolationLevel);

            return this;
        }
    }
}