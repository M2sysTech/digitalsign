namespace Veros.Data.Providers
{
    using FluentNHibernate.Cfg.Db;
    using Veros.Data;

    public class SQLiteDatabaseProvider : DatabaseProviderBase
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
                return "@p";
            }
        }

        public override string Name
        {
            get { return "SQLite"; }
        }

        public override ConnectionString ParseConnectionString(string connectionString)
        {
            return this.InternalParseConnectionString(connectionString);
        }

        public override string GetDateTimeQuery()
        {
            throw new System.NotImplementedException();
        }

        public override IPersistenceConfigurer GetHibernateConfiguration()
        {
            return SQLiteConfiguration.Standard.ConnectionString(Database.ConnectionString);
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
