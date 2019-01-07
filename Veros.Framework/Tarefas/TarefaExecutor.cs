namespace Veros.Framework.Tarefas
{
    using System;
    using System.Linq;
    using Performance;
    using Veros.Framework;

    public class TarefaExecutor
    {
        public void Executar(string[] args)
        {
            var tarefas = IoC.Current.GetAllInstances<ITarefaM2>().ToArray();
            var controller = new TarefaController(new HelpTarefa(), tarefas);

            try
            {
                var medicao = new Medicao();
                controller.Executar(args);

                Log.Application.InfoFormat(" ");
                Log.Application.InfoFormat("Executado em {0}", medicao.MostrarSegundos());
            }
            catch (Exception exception)
            {
                Log.Application.ErrorFormat(exception, "Não foi possível executar comando: {0}", args.Join(" "));
            }
        }
    }
}