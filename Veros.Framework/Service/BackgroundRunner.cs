namespace Veros.Framework.Service
{
    using System;
    using System.Timers;

    /// <summary>
    /// Executa um trabalho em background em um intervalo
    /// </summary>
    public class BackgroundRunner
    {
        private Timer timer;

        public void Stop()
        {
            this.timer.Enabled = false;
            this.timer.Stop();
            this.timer.Dispose();

            Log.Framework.Debug("Timer parado");
        }

        public void Start(TimeSpan interval, string taskName, Action action, bool executeAtStart = true)
        {
            if (executeAtStart)
            {
                Log.Framework.DebugFormat("Executando tarefa '{0}' antes de iniciar timer", taskName);
                action();
            }

            this.timer = new Timer();

            this.timer.Elapsed += (sender, e) =>
            {
                Log.Application.DebugFormat(
                    "Intervalo de {0}s atingido. Executando tarefa '{1}'",
                    interval.TotalSeconds,
                    taskName);

                this.Stop();

                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Log.Application.ErrorFormat(ex, "Ocorreu um erro ao executar tarefa '{0}'", taskName);
                }

                this.Start(interval, taskName, action, false);
            };

            this.timer.AutoReset = false;
            this.timer.Interval = interval.TotalMilliseconds;
            this.timer.Enabled = true;
            this.timer.Start();

            Log.Framework.DebugFormat(
                "Timer da tarefa '{0}' iniciado. Esperando {1} segundos",
                taskName,
                interval.TotalSeconds);
        }
    }
}
