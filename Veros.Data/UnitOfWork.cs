namespace Veros.Data
{
    using System;
    using System.Data;
    using Framework;
    using NHibernate;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IUnitOfWorkTransactionFactory transactionFactory;
        private readonly IUnitOfWorkStore store;

        public UnitOfWork(
            IUnitOfWorkTransactionFactory transactionFactory,
            IUnitOfWorkStore store)
        {
            this.transactionFactory = transactionFactory;
            this.store = store;
        }

        public IUnitOfWorkTransaction Current
        {
            get
            {
                if (this.ExistTransaction)
                {
                    return this.store.Current;
                }

                throw new InvalidOperationException("You are not in a unit of work");
            }
        }

        public bool Begun
        {
            get { return this.ExistTransaction; }
        }

        private bool ExistTransaction
        {
            get
            {
                return this.store.Current != null;
            }
        }

        public IUnitOfWorkTransaction Begin(
            int batchSize = 1,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            FlushMode flushMode = FlushMode.Commit)
        {
            if (this.ExistTransaction)
            {
                throw new InvalidOperationException(
                    "You cannot start more than one unit of work at the same time");
            }

            var transaction = this.transactionFactory.Create(isolationLevel, flushMode);
            
            //// TODO: habilitar batch
            ////if (Database.Provider.SupportBatch)
            ////{
            ////    transaction.CurrentSession.SetBatchSize(batchSize);
            ////}

            this.store.Current = transaction;
            return transaction;
        }

        /// <summary>
        /// Abre uma transação com o banco de dados, executa e retorna o valor da função informada em func
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public T Obter<T>(Func<T> func)
        {
            using (var trans = this.Begin())
            {
                try
                {
                    var item = func();
                    trans.Commit();
                    return item;
                }
                catch
                {
                    trans.RollBack();
                    throw;
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        public void Transacionar(Action action, FlushMode flushMode = FlushMode.Commit)
        {
            using (var trans = this.Begin(flushMode: flushMode))
            {
                try
                {
                    action();
                    trans.Commit();
                }
                catch
                {
                    trans.RollBack();
                    throw;
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }
    }
}