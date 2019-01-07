//-----------------------------------------------------------------------
// <copyright file="Database.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data
{
    using Framework;

    public class Database
    {
        private static DatabaseConfiguration config;
        private static bool initialized;
        private static ConnectionString connection;
        private static IDatabaseSchema databaseSchema;
        private static IDatabaseProvider provider;
        private static string connectionString;
        private static IDatabaseSeed databaseSeed;

        public static DatabaseConfiguration Config
        {
            get { return Return(config); }
        }

        public static string ProviderName
        {
            get { return Return(Database.provider.Name); }
        }

        public static string ConnectionString
        {
            get { return Return(connectionString); }
        }

        public static ConnectionString Connection
        {
            get { return Return(connection); }
        }

        public static IDatabaseSchema Schema
        {
            get
            {
                ThrowExceptionIfWasNotInitialized();
                
                if (databaseSchema == null)
                {
                    databaseSchema = IoC.Current.Resolve<IDatabaseSchema>();
                }

                return databaseSchema;
            }
        }

        public static IDatabaseSeed Seed
        {
            get
            {
                ThrowExceptionIfWasNotInitialized();

                if (databaseSeed == null)
                {
                    databaseSeed = IoC.Current.Resolve<IDatabaseSeed>();
                }

                return databaseSeed;
            }
        }

        public static IDatabaseProvider Provider
        {
            get { return Return(provider); }
        }

        public static string Onwer
        {
            get;
            set;
        }

        public static void Initialize(IDatabaseProvider provider, string connectionString, string owner, bool force = false)
        {
            if (initialized && force == false)
            {
                return;
            }

            Log.Application.Debug("Carregando configurações do banco de dados");

            Database.connectionString = Veros.Data.ConnectionString.DecryptographyIfIsCryptographed(connectionString);
            Database.provider = provider;
            Database.connection = provider.ParseConnectionString(Database.connectionString);
            Database.config = new DatabaseConfiguration();
            Database.Onwer = owner;

            Log.Application.DebugFormat(
                "Provider {0}, ConnectionString: {1}", 
                provider.Name, 
                connection.GetWithoutPassword());

            SqlAppender.ParameterChar = provider.ParameterChar;

            initialized = true;
        }

        public static void RestartSettings(string ownerConnectionString, string owner)
        {
            Log.Application.DebugFormat("Reiniciando configurações do banco de dados para usuario {0}", owner);

            Database.connectionString = Veros.Data.ConnectionString.DecryptographyIfIsCryptographed(ownerConnectionString);
            Database.connection = provider.ParseConnectionString(Database.connectionString);
            Database.Onwer = owner;

            Log.Application.DebugFormat(
                "Provider {0}, ConnectionString: {1}",
                provider.Name,
                connection.GetWithoutPassword());

            initialized = true;
        }

        private static void ThrowExceptionIfWasNotInitialized()
        {
            if (initialized == false)
            {
                throw new DatabaseConfigurationException("Framework de dados não foi inicializado. Você deve chamar Database.Initialize antes de acessar o banco de dados");
            }
        }

        private static T Return<T>(T value)
        {
            ThrowExceptionIfWasNotInitialized();
            return value;
        }
    }
}
