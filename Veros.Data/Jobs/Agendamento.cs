namespace Veros.Data.Jobs
{
    using System;
    using Quartz;
    using Veros.Framework;

    public class Agendamento<TJob> where TJob : IJob
    {
        private readonly IJobDetail job;
        private readonly ITrigger trigger;
        private IScheduler scheduler;
        
        public Agendamento(TimeSpan interval, int prioridade = 10)
        {
            var name = typeof(TJob).Name;
            var groupName = name + "Group";

            Log.Application.DebugFormat(
                "Configurando agendamento para {0} a cada {1}s",
                name,
                interval.TotalSeconds);

            this.job = JobBuilder.Create<TJob>()
                .WithIdentity(name, groupName)
                .Build();

            this.trigger = TriggerBuilder.Create()
                .WithIdentity(name + "Trigger", groupName)
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(interval.TotalSeconds.ToInt())
                    .RepeatForever()
                    .WithMisfireHandlingInstructionIgnoreMisfires())
                .WithPriority(prioridade)
                .Build();
        }

        public void Start()
        {
            this.scheduler = Agendamentos.Scheduler;
            this.scheduler.ScheduleJob(this.job, this.trigger);
        }

        public void Stop()
        {
            this.scheduler.UnscheduleJob(this.trigger.Key);
        }
    }
}
