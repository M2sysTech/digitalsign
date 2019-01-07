namespace Veros.Data.Tarefas
{
    using Veros.Framework;

    public class SeedExecuteTarefa : ITarefaM2
    {
        public string TextoDeAjuda
        {
            get
            {
                return "Popula tabelas do banco de dados com dados fixos";
            }
        }

        public string Comando
        {
            get
            {
                return "seed execute";
            }
        }

        public void Executar(params string[] args)
        {
            Log.Application.Info("Executando Seed");
            Database.Seed.Executar();
            Log.Application.Info("Seed executado com sucesso");
        }
    }
}
