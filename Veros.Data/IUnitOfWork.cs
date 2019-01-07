namespace Veros.Data
{
    using System;
    using System.Data;
    using NHibernate;

    public interface IUnitOfWork
    {
        IUnitOfWorkTransaction Current
        {
            get;
        }

        bool Begun
        {
            get;
        }

        IUnitOfWorkTransaction Begin(
            int batchSize = 0,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            FlushMode flushMode = FlushMode.Commit);

        void Transacionar(Action action, FlushMode flushMode = FlushMode.Commit);

        /// <summary>
        /// Abre uma transação com o banco de dados, executa e retorna o valor da função informada em func
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        T Obter<T>(Func<T> func);
    }
}
