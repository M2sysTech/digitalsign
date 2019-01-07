namespace Veros.Framework.Tarefas
{
    using System;
    using System.Linq;
    using System.Text;
    using Veros.Framework;

    public class HelpTarefa : ITarefaM2
    {
        public string TextoDeAjuda
        {
            get
            {
                return "Ajuda";
            }
        }

        public string Comando
        {
            get
            {
                return "help";
            }
        }

        public void Executar(params string[] args)
        {
            const string Help = @"M2.exe - Utilitario de tarefas

Uso:
    m2 [TAREFA] [PARAMETROS DA TAREFA] [OPCOES]

Tarefas:

{0}
Opcoes:
    --debug     Ativa log nivel Debug
";
            var ajudaTarefas = new StringBuilder();
            var tarefas = IoC.Current.GetAllInstances<ITarefaM2>().OrderBy(x => x.Comando);

            foreach (var tarefa in tarefas)
            {
                ajudaTarefas.Append("   ");
                ajudaTarefas.Append(tarefa.Comando);
                ajudaTarefas.Append(Environment.NewLine);
                ajudaTarefas.Append("       ");
                ajudaTarefas.Append(tarefa.TextoDeAjuda);
                ajudaTarefas.Append(Environment.NewLine);
                ajudaTarefas.Append(Environment.NewLine);
            }

            Log.Application.Info(string.Format(Help, ajudaTarefas));
        }
    }
}