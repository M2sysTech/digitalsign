namespace SignService.Boot
{
    using System;
    using Veros.Data;
    using Veros.Framework;
    using Veros.Framework.Service;

    public class ConfiguracoesBoot : IConfiguracaoBoot
    {
        public void Execute()
        {
            var connectionString = Database.ConnectionString;

            if (connectionString.ToLower().Contains("gedcef") == false)
            {
                Log.Application.Debug(
                    "Você não está usando um banco de dados gedcef. Informe uma base na string de conexão no arquivo settings.config");

                Environment.Exit(0);
            }
        }
    }
}