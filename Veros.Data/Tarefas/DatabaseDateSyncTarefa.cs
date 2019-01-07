namespace Veros.Data.Tarefas
{
    using System;
    using System.Runtime.InteropServices;
    using NHibernate;
    using Repositories;
    using Veros.Data;
    using Veros.Framework;

    public class DatabaseDateSyncTarefa : ITarefaM2
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDatabaseRepository databaseRepository;

        public DatabaseDateSyncTarefa(IUnitOfWork unitOfWork, IDatabaseRepository databaseRepository)
        {
            this.unitOfWork = unitOfWork;
            this.databaseRepository = databaseRepository;
        }

        public string TextoDeAjuda
        {
            get
            {
                return "Sincroniza data e hora da maquina com o banco de dados";
            }
        }

        public string Comando
        {
            get
            {
                return "database date sync";
            }
        }

        public void Executar(params string[] args)
        {
            if (Aplicacao.EstaRodandoComoAdministrador() == false)
            {
                Log.Application.Fatal("Nao foi possivel executar o comando");
                Log.Application.Fatal("Voce deve ter permissoes de Administrador da maquina");
                return;
            }

            using (this.unitOfWork.Begin())
            {
                Log.Application.InfoFormat(
                    "Data e hora da maquina local: {0}",
                    DateTime.Now);

                var databaseDateTime = this.databaseRepository.GetDateTime();
                
                Log.Application.InfoFormat(
                    "Data e hora do {0} {1}: {2}",
                    Database.ProviderName,
                    Database.Connection.Database,
                    databaseDateTime);

                this.UpdateDate(databaseDateTime);
                this.UpdateTime(databaseDateTime);

                Log.Application.InfoFormat("Sincronização executada. Confira se foi sincronizado corretamente. Se nao, verifique se executou como Administrador da maquina");
            }
        }

        private void UpdateDate(DateTime databaseDateTime)
        {
            var dateString = databaseDateTime.ToString("dd/MM/yyyy");
            Log.Application.InfoFormat("Alterando DATA da maquina para {0}", dateString);
            this.ExecuteCommand("/c date " + dateString);
        }

        private void UpdateTime(DateTime databaseDateTime)
        {
            var timeString = databaseDateTime.ToString("HH:mm:ss");
            Log.Application.InfoFormat("Alterando HORA da maquina para {0}", timeString);
            this.ExecuteCommand("/c time " + timeString);
        }

        private void ExecuteCommand(string command)
        {
            Log.Application.Debug("Executando comando time");

            var p = new System.Diagnostics.Process
            {
                StartInfo =
                {
                    WorkingDirectory = "c:\\Windows",
                    UseShellExecute = false,
                    FileName = "cmd.exe",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            
            p.Start();
            p.Close();
        }
    }
}