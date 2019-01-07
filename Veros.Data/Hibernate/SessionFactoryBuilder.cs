//-----------------------------------------------------------------------
// <copyright file="SessionFactoryBuilder.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Hibernate
{
    using NHibernate;

    public class SessionFactoryBuilder : ISessionFactoryBuilder
    {
        private static ISessionFactory sessionFactory;
        private readonly IConfigurationBuilder configurationBuilder;
        
        public SessionFactoryBuilder(IConfigurationBuilder configurationBuilder)
        {
            this.configurationBuilder = configurationBuilder;
        }

        public ISessionFactory GetSessionFactory()
        {
            return sessionFactory ??
                   (sessionFactory = this.configurationBuilder.Build().BuildSessionFactory());
        }
    }
}
