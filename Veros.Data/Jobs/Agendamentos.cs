namespace Veros.Data.Jobs
{
    using System;
    using System.Collections.Specialized;
    using Quartz;
    using Quartz.Impl;
    using System.Threading.Tasks;

    public class Agendamentos
    {
        private static IScheduler scheduler;

        public static IScheduler Scheduler
        {
            get
            {
                if (scheduler == null)
                {
                    Configurar(1);
                }

                return scheduler;
            }
        }

        /// <summary>
        /// Configura todos os agendamentos de jobs
        /// </summary>
        /// <param name="maxWorkers">Número máximo de threads</param>
        public static async void Configurar(int maxWorkers)
        {
            if (scheduler != null)
            {
                throw new Exception("Configuração para todos os agendamentos já foi realizado");
            }

            var properties = new NameValueCollection
            {
                { "quartz.threadPool.threadCount", maxWorkers.ToString() }
            };

            scheduler = await new StdSchedulerFactory(properties).GetScheduler();
            scheduler.JobFactory = new StructureMapJobFactory();
            await scheduler.Start();
        }

        public static void PararTodos()
        {
            Scheduler.Shutdown();
        }
    }
}