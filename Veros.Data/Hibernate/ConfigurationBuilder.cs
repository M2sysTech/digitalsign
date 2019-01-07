//-----------------------------------------------------------------------
// <copyright file="ConfigurationBuilder.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Hibernate
{
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private readonly IDatabaseProvider databaseProvider;

        public ConfigurationBuilder(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        public IHibernateConfiguration Build()
        {
            var configuration = new HibernateConfiguration(this.databaseProvider);

#if DEBUG
            configuration.Properties.Add("generate_statistics", "true");
#endif

            return configuration;
        }
    }
}
