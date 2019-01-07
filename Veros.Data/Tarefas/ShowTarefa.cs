namespace Veros.Data.Tarefas
{
    using Veros.Data;
    using Veros.Framework;

    public class ShowTarefa : ITarefaM2
    {
        public string TextoDeAjuda
        {
            get
            {
                return "Mostra configurações do sistema";
            }
        }

        public string Comando
        {
            get
            {
                return "show";
            }
        }

        public void Executar(params string[] args)
        {
            Log.Application.Info("AppSettings:");
            Log.Application.Info(string.Empty);
            Log.Application.Info("Provider: " + Database.ProviderName);
            Log.Application.Info("ConnectionString: " + Database.Connection.GetWithoutPassword());
            Log.Application.Info("Log: " + Log.Configuracao.Level);
        }
    }
}