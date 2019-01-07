namespace Veros.Data.Boot
{
    using System.Configuration;
    using Framework;
    using Providers;
    using Seeds;
    using Veros.Data;
    using Veros.Framework.DependencyResolver;
    using StructureMap.Graph;

    public class RegistraDependencias : Registrar<RegistraDependencias>
    {
        public override void Registros(IAssemblyScanner scan)
        {
            scan.AddAllTypesOf<IDatabaseProvider>();
            scan.AddAllTypesOf<Seed>();

            this.InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            var providerName = ConfigurationManager.AppSettings["Database.Provider"];
            var connectionString = ConfigurationManager.AppSettings["Database.ConnectionString"];
            var owner = ConfigurationManager.AppSettings["Database.Owner"];
            
            IDatabaseProvider databaseProvider = new PostgreDatabaseProvider();

            this.For<IDatabaseProvider>().AddInstances(x =>
            {
                var providers = Reflection.GetInstancesOfImplementingTypes<IDatabaseProvider>(this.GetAssembly());

                foreach (var provider in providers)
                {
                    if (provider.Name == providerName)
                    {
                        databaseProvider = provider;
                    }

                    x.Type(provider.GetType()).Named(provider.Name);
                }
            });

            this.For<IDatabaseProvider>().Use(databaseProvider);

            Database.Initialize(databaseProvider, connectionString, owner);
        }
    }
}