namespace Veros.Framework.LogHelpers
{
    using log4net.Repository;
    using log4net.Repository.Hierarchy;

    internal class HibernateLogger : BaseLogger
    {
        public HibernateLogger(LogConfiguration configuration, ILoggerRepository loggerRepository)
            : base(configuration, loggerRepository)
        {
            this.Logger = (Logger)loggerRepository.GetLogger("NHibernate.SQL");
        }

        public override bool ShouldBeEnable
        {
            get
            {
                return this.configuration.SqlAtivo;
            }
        }
    }
}