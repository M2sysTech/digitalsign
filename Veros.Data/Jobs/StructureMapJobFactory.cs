namespace Veros.Data.Jobs
{
    using Framework;
    using Quartz;
    using Quartz.Spi;

    public class StructureMapJobFactory : IJobFactory
    {
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                return (IJob)IoC.Current.Resolve(bundle.JobDetail.JobType);
            }
            catch (System.Exception ex)
            {
                Log.Application.Error(ex);
                throw;
            }
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
