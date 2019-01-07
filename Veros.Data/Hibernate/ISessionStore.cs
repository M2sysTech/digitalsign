//-----------------------------------------------------------------------
// <copyright file="ISessionStore.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Hibernate
{
    using NHibernate;

    public interface ISessionStore
    {
        ISession Current
        {
            get;
            set;
        }

        void Clear();
    }
}
