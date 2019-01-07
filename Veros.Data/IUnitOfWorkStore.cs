//-----------------------------------------------------------------------
// <copyright file="IUnitOfWorkStore.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data
{
    public interface IUnitOfWorkStore
    {
        IUnitOfWorkTransaction Current
        {
            get;
            set;
        }

        void Clear();
    }
}
