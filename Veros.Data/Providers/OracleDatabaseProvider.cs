namespace Veros.Data.Providers
{
    using System;
    using System.IO;
    using FluentNHibernate.Cfg.Db;
    using Framework;

    public class OracleDatabaseProvider : DatabaseProviderBase
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
            get { return "Oracle"; }
        }

        public override ConnectionString ParseConnectionString(string connectionString)
        {
            return this.InternalParseConnectionString(connectionString, serverKey: "Data Source", databaseKey: "User Id", userKey: "Nenhum");
        }

        public override string GetDateTimeQuery()
        {
            return "SELECT SYSDATE datahora FROM dual";
        }

        public override IPersistenceConfigurer GetHibernateConfiguration()
        {
            return OracleDataClientConfiguration.Oracle10.ConnectionString(Database.ConnectionString);
        }

        public override void GetDriver()
        {
            var programFilePath = Aplicacao.ObterPastaVeros();

            var oracleDriversPath = Path.Combine(programFilePath, "Oracle");
            
            string bitProcess;

            if (OracleManagedDriver.Versao10)
            {
                bitProcess = "10ix86";
            }
            else
            {
                bitProcess = Environment.Is64BitProcess ? "x64" : "x86";
            }

            var oracleBitProcessDriversPath = Path.Combine(oracleDriversPath, bitProcess);

            if (Directory.Exists(oracleBitProcessDriversPath) == false)
            {
                throw new DatabaseConfigurationException(string.Format(
                    "Não foi encontrado pasta com os drivers do Oracle para .NET: {0}. Verifique se o pacote da M2Sys com os drivers do Oracle foi instalado nesta máquina.",
                    oracleDriversPath));
            }

            foreach (var file in Directory.GetFiles(oracleBitProcessDriversPath, "*.*", SearchOption.AllDirectories))
            {
                var driverFile = Path.GetFileName(file);
                var destinationFile = string.IsNullOrEmpty(Aplicacao.Caminho) ? driverFile : Path.Combine(Aplicacao.Caminho, driverFile);

                if (File.Exists(destinationFile) == false)
                {
                    Log.Application.DebugFormat("Copiando {0}", driverFile);
                    File.Copy(file, destinationFile);
                }
            }
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
