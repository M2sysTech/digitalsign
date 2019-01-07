namespace Veros.Framework.Tarefas
{
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Framework;
    using log4net.Core;

    public class TarefaController
    {
        private readonly List<ITarefaM2> tarefas;
        private readonly ITarefaM2 tarefaHelp;

        public TarefaController(ITarefaM2 tarefaHelp, params ITarefaM2[] tarefas)
        {
            this.tarefas = tarefas.Where(x => x.GetType() != typeof(HelpTarefa)).ToList();
            this.tarefaHelp = tarefaHelp;
            this.tarefas.Add(tarefaHelp);
        }

        public static bool Match(string comando, string argumentos)
        {
            var comandoArray = comando.SepararPor(" ");
            var argumentosArray = argumentos.SepararPor(" ");

            if (comandoArray.Length > argumentosArray.Length)
            {
                return false;
            }

            return !comandoArray.Where((t, x) => t != argumentosArray[x]).Any();
        }

        public void Executar(params string[] args)
        {
            var textoArgumento = args.Join(" ");
            var tarefa = this.ObterTarefa(textoArgumento);

            Log.Application.DebugFormat("Comando: {0}", textoArgumento);
            
            if (tarefa != null)
            {
                Log.Application.DebugFormat("Executando tarefa: {0}", tarefa.GetType().FullName);
                Log.Application.DebugFormat(" ");

                Log.Configurar(new LogConfiguration
                {
                    ConsoleAtivo = true,
                    Level = args.Contains("--debug") ? Level.Debug : Level.Info,
                    NomeDoArquivo = tarefa.GetType().Name
                });

                tarefa.Executar(args);    
            }
            else
            {
                Log.Application.DebugFormat("Não foi encontrado tarefa. Mostrando help");
                this.tarefaHelp.Executar("help");
            }
        }

        private ITarefaM2 ObterTarefa(string textoArgumentos)
        {
            return this.tarefas.FirstOrDefault(tarefa => Match(tarefa.Comando, textoArgumentos));
        }
    }
}
