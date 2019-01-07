//-----------------------------------------------------------------------
// <copyright file="UnitOfWorkStore.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data
{
    using Framework.Threads;

    public class UnitOfWorkStore : IUnitOfWorkStore
    {
        private const string CurrentUnitOfWorkKey = "CurrentUnitOfWork.Key";
        private readonly ILocalData localData;

        public UnitOfWorkStore(ILocalData localData)
        {
            this.localData = localData;
        }

        public IUnitOfWorkTransaction Current
        {
            get { return (IUnitOfWorkTransaction) this.localData[CurrentUnitOfWorkKey]; }
            set { this.localData[CurrentUnitOfWorkKey] = value; }
        }

        public void Clear()
        {
            this.Current = null;
        }
    }
}