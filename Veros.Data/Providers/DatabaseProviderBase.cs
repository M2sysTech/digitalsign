namespace Veros.Data.Providers
{
    using System.Configuration;
    using System.Data.Common;
    using FluentNHibernate.Cfg.Db;
    using Veros.Data;

    public abstract class DatabaseProviderBase : IDatabaseProvider
    {
        public bool ShouldExtractDriver
        {
            get
            {
                var setting = ConfigurationManager.AppSettings["Database.ExtractDriver"];

                if (setting == null)
                {
                    return true;
                }

                bool shouldExtract;
                bool.TryParse(setting, out shouldExtract);
                return shouldExtract;
            }
        }

        public abstract bool SupportBatch
        {
            get;
        }

        public abstract string ParameterChar
        {
            get;
        }

        public abstract string Name
        {
            get;
        }

        public abstract string True();

        public abstract string False();

        public abstract IPersistenceConfigurer GetHibernateConfiguration();
        
        public abstract void GetDriver();

        public abstract ConnectionString ParseConnectionString(string connectionString);

        public abstract string GetDateTimeQuery();

        protected ConnectionString InternalParseConnectionString(
            string connectionText,
            string serverKey = "Server",
            string portKey = "Port",
            string databaseKey = "Database",
            string userKey = "User Id",
            string passwordKey = "Password")
        {
            var connBuilder = new DbConnectionStringBuilder(false);
            connBuilder.ConnectionString = connectionText;

            return new ConnectionString(connectionText)
            {
                Server = connBuilder.ContainsKey(serverKey) ? connBuilder[serverKey].ToString() : string.Empty,
                Port = connBuilder.ContainsKey(portKey) ? connBuilder[portKey].ToString() : string.Empty,
                Database = connBuilder.ContainsKey(databaseKey) ? connBuilder[databaseKey].ToString() : string.Empty,
                User = connBuilder.ContainsKey(userKey) ? connBuilder[userKey].ToString() : string.Empty,
                Password = connBuilder.ContainsKey(passwordKey) ? connBuilder[passwordKey].ToString() : string.Empty,
            };
        }
    }
}
