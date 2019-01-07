//-----------------------------------------------------------------------
// <copyright file="IHibernateConfiguration.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Hibernate
{
    using System.Collections.Generic;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Event;

    public interface IHibernateConfiguration
    {
        Configuration Configuration
        {
            get;
        }

        IDictionary<string, string> Properties
        {
            get;
        }

        IDictionary<ListenerType, object> Listener
        {
            get;
        }
        
        ISessionFactory BuildSessionFactory();
    }
}
