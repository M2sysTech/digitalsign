//-----------------------------------------------------------------------
// <copyright file="HibernateConfiguration.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Hibernate
{
    using System;
    using System.Collections.Generic;
    using Enumerations;
    using FluentNHibernate.Cfg;
    using NHibernate.Cfg;
    using Veros.Framework;
    using NHibernate;
    using NHibernate.Event;

    public class HibernateConfiguration : IHibernateConfiguration
    {
        private readonly IDatabaseProvider databaseProvider;
        private readonly NHibernate.Cfg.Configuration configuration = new NHibernate.Cfg.Configuration();
        private readonly IDictionary<string, string> properties = new Dictionary<string, string>();
        private readonly IDictionary<ListenerType, object> listeners = new Dictionary<ListenerType, object>();

        public HibernateConfiguration(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        public static Configuration BuiltConfiguration
        {
            get;
            private set;
        }

        public NHibernate.Cfg.Configuration Configuration
        {
            get { return this.configuration; }
        }

        public IDictionary<string, string> Properties
        {
            get { return this.properties; }
        }

        public IDictionary<ListenerType, object> Listener
        {
            get { return this.listeners; }
        }

        public ISessionFactory BuildSessionFactory()
        {
            this.configuration.AddProperties(this.properties);
            this.listeners.ForEach(x => this.configuration.SetListener(x.Key, x.Value));

            var mappings = this.GetMappingsAssemblies();

            if (this.databaseProvider.ShouldExtractDriver)
            {
                this.databaseProvider.GetDriver();
            }

            BuiltConfiguration = Fluently
                .Configure()
                .Database(this.databaseProvider.GetHibernateConfiguration)
                .Mappings(mappings)
                .ExposeConfiguration(this.SetCustomSettings)
                .BuildConfiguration();

            var sessionFactory = BuiltConfiguration.BuildSessionFactory();

            Log.Application.DebugFormat("Foi construida session factory {0}", sessionFactory.GetHashCode());

            return sessionFactory;
        }

        private void SetCustomSettings(Configuration configuration)
        {
            foreach (var hibernateSetting in Database.Config.HibernateSettings)
            {
                configuration.SetProperty(
                    hibernateSetting.Key,
                    hibernateSetting.Value);
            }
        }

        private Action<MappingConfiguration> GetMappingsAssemblies()
        {
            Action<MappingConfiguration> mappings = x =>
            {
                var mapping = x.FluentMappings;
                Database.Config.Mappings.ForEach(a => mapping.AddFromAssembly(a));
                mapping.Conventions.Add<EnumConvention>();
                mapping.Conventions.Add<EnumerationStringTypeConvention>();
                mapping.Conventions.Add<EnumerationIntTypeConvention>();
            };

            return mappings;
        }
    }
}
