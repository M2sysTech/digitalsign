namespace Veros.Data.Providers
{
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using Framework.Security;
    using NHibernate;
    using Veros.Data;

    public class OracleGuestDatabaseProvider : OracleDatabaseProvider
    {
        private readonly string owner;

        public OracleGuestDatabaseProvider()
        {
        }

        public OracleGuestDatabaseProvider(string owner)
        {
            this.owner = owner;
        }

        public override string Name
        {
            get { return "OracleGuest"; }
        }

        public override IPersistenceConfigurer GetHibernateConfiguration()
        {
            var guestConfiguration = OracleDataClientConfiguration.Oracle10.ConnectionString(Database.ConnectionString);

            string ownerConnectionString;

            using (var sessionFactory = Fluently.Configure().Database(guestConfiguration).BuildSessionFactory())
            {
                using (var session = sessionFactory.OpenSession())
                {
                    var senhaBancoDados = session
                        .CreateSQLQuery("SELECT PWDS_PWDDB FROM PWDS WHERE PWDS_OWNER = :owner")
                        .AddScalar("PWDS_PWDDB", NHibernateUtil.String)
                        .SetParameter("owner", Database.Onwer.ToUpper())
                        .UniqueResult();

                    if (senhaBancoDados == null || string.IsNullOrEmpty(senhaBancoDados.ToString()))
                    {
                        throw new DatabaseConfigurationException(
                            string.Format("Senha do owner {0} não foi encontrado na tabela PWDS", this.owner));
                    }

                    var password = new SimpleCryptography().Decode(senhaBancoDados.ToString());

                    ownerConnectionString = this.BuildConnectionString(new ConnectionString
                    {
                        Server = Database.Connection.Server,
                        User = Database.Onwer,
                        Password = password
                    });
                }
            }

            Database.RestartSettings(ownerConnectionString, Database.Onwer);
            return OracleDataClientConfiguration.Oracle10.ConnectionString(ownerConnectionString);
        }

        private string BuildConnectionString(ConnectionString connectionString)
        {
            return string.Format(
                "Data Source={0};User Id={1};Password={2};",
                connectionString.Server,
                connectionString.User,
                connectionString.Password);
        }
    }
}
