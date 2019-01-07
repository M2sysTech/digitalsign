namespace Veros.Data
{
    using System.Data.Common;
    using Framework.Security;

    public class ConnectionString
    {
        private readonly string connectionString = string.Empty;

        public ConnectionString()
        {
        }

        public ConnectionString(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string Server
        {
            get;
            set;
        }

        public string Database
        {
            get;
            set;
        }

        public string User
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string Port
        {
            get;
            set;
        }

        public static string DecryptographyIfIsCryptographed(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                return string.Empty;
            }

            if (connectionString.StartsWith("Cripto"))
            {
                return new SimplerAes().Decode(connectionString.Remove(0, 6));
            }

            return connectionString;
        }

        public string GetWithoutPassword()
        {
            var conn = new DbConnectionStringBuilder(false);
            conn.ConnectionString = this.connectionString;
            conn.Remove("Password");
            conn.Remove("Pwd");
            return conn.ToString();
        }

        public string GetWithPassword()
        {
            return this.connectionString;
        }
    }
}
