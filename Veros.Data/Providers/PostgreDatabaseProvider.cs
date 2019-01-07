namespace Veros.Data.Providers
{
    using FluentNHibernate.Cfg.Db;
    using Veros.Data;

    public class PostgreDatabaseProvider : DatabaseProviderBase
    {
        public override bool SupportBatch
        {
            get
            {
                return false;
            }
        }

        public override string ParameterChar
        {
            get
            {
                return ":p";
            }
        }

        public override string Name
        {
            get { return "Postgre"; }
        }
        
        public override ConnectionString ParseConnectionString(string connectionString)
        {
            return this.InternalParseConnectionString(connectionString);
        }

        public override string GetDateTimeQuery()
        {
            return "SELECT now()";
        }

        public override IPersistenceConfigurer GetHibernateConfiguration()
        {
            return PostgreSQLConfiguration
                .PostgreSQL82
                .ConnectionString(Database.ConnectionString);
        }

        public override void GetDriver()
        {
        }

        public override string True()
        {
            return "true";
        }

        public override string False()
        {
            return "false";
        }
    }
}
