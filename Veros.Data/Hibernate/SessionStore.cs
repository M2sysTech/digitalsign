//-----------------------------------------------------------------------
// <copyright file="SessionStore.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Hibernate
{
    using Framework.Threads;
    using NHibernate;

    public class SessionStore : ISessionStore
    {
        private const string SessionKey = "NHibernate.ISession";
        private readonly ILocalData localData;

        public SessionStore(ILocalData localData)
        {
            this.localData = localData;
        }

        public ISession Current
        {
            get { return (ISession)this.localData[SessionKey]; }
            set { this.localData[SessionKey] = value; }
        }

        public void Clear()
        {
            this.Current = null;
        }
    }
}