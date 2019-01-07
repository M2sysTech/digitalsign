namespace Veros.Data.Tarefas
{
    using System;
    using Veros.Data;
    using Veros.Framework;

    public class DatabaseMigrateTarefa : ITarefaM2
    {
        private readonly IDatabaseSchema databaseSchema = IoC.Current.Resolve<IDatabaseSchema>();

        public string TextoDeAjuda
        {
            get
            {
                return "Atualiza schema do banco de dados";
            }
        }

        public string Comando
        {
            get
            {
                return "database migrate";
            }
        }

        public void Executar(params string[] args)
        {
            Log.Application.InfoFormat(@"Atualizando banco de dados... Não interrompa, aguarde.");
            this.databaseSchema.AtualizarParaUltimoMigration();
        }
    }
}