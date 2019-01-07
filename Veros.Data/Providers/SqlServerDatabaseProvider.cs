namespace Veros.Data.Providers
{
    using FluentNHibernate.Cfg.Db;
    using Veros.Data;

    public class SqlServerDatabaseProvider : DatabaseProviderBase
    {
        public override bool SupportBatch
        {
            get { return true; }
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
            get { return "SqlServer"; }
        }

        public override ConnectionString ParseConnectionString(string connectionString)
        {
            return this.InternalParseConnectionString(connectionString, serverKey: "Data Source");
        }

        public override string GetDateTimeQuery()
        {
            return "SELECT GETDATE()";
        }

        public override IPersistenceConfigurer GetHibernateConfiguration()
        {
            return MsSqlConfiguration.MsSql2005.ConnectionString(Database.ConnectionString);
        }

        public override void GetDriver()
        {
        }

        public override string True()
        {
            return "1";
        }

        public override string False()
        {
            return "0";
        }
    }
}
