//-----------------------------------------------------------------------
// <copyright file="SessionBuilder.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Hibernate
{
    using Veros.Framework;
    using NHibernate;

    public class SessionBuilder : ISessionBuilder
    {
        private readonly ISessionFactoryBuilder sessionFactoryBuilder;
        private readonly ISessionStore sessionStore;

        public SessionBuilder(ISessionFactoryBuilder sessionFactoryBuilder, ISessionStore sessionStore)
        {
            this.sessionFactoryBuilder = sessionFactoryBuilder;
            this.sessionStore = sessionStore;
        }

        public ISession GetSession()
        {
            var session = this.sessionStore.Current;

            if (session == null || session.IsOpen == false)
            {
                return this.CreateSession();
            }

            return session;
        }

        private ISession CreateSession()
        {
            var session = this.sessionFactoryBuilder.GetSessionFactory().OpenSession();
            session.FlushMode = FlushMode.Commit;
            this.sessionStore.Current = session;

            Log.Application.DebugFormat("Begin trans {0}", session.GetHashCode());

            return session;
        }
    }
}
