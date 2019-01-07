namespace Veros.Data.Jobs
{
    using System;
    using System.Threading.Tasks;
    using Framework;
    using Quartz;

    public abstract class Job : IJob
    {
        protected IJobExecutionContext context;

        protected string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public abstract void Execute();

        async Task IJob.Execute(IJobExecutionContext context)
        {
            Log.Application.InfoFormat("Executando job {0}", this.Name);

            try
            {
                this.context = context;
                Execute();
            }
            catch (Exception ex)
            {
                Log.Application.ErrorFormat(ex, "Erro ao processar job {0}", context.JobDetail.Key.Name);
            }

            Log.Application.InfoFormat("Fim da execução de job {0}", this.Name);
        }
    }
}
