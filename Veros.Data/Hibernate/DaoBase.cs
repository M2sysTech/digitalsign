namespace Veros.Data.Hibernate
{
    using NHibernate;
    using Veros.Data;
    using Veros.Framework;

    public abstract class DaoBase
    {
        protected readonly IUnitOfWork unitOfWork;

        protected DaoBase()
        {
            this.unitOfWork = IoC.Current.Resolve<IUnitOfWork>();
        }

        /// <summary>
        /// Gets uma sessão do nhibernate
        /// </summary>
        protected ISession Session
        {
            get { return this.unitOfWork.Current.CurrentSession; }
        }
    }
}
