namespace Veros.Data.Providers
{
    using FluentNHibernate.Cfg.Db;

    public class OracleManagedConfiguration : PersistenceConfiguration<OracleManagedConfiguration, OracleConnectionStringBuilder>
    {
        public OracleManagedConfiguration()
        {
            Driver<OracleManagedDriver>();
        }

        public static OracleManagedConfiguration Managed
        {
            get
            {
                return new OracleManagedConfiguration().Dialect<NHibernate.Dialect.Oracle10gDialect>();
            }
        }
    }
}
