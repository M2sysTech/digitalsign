namespace Veros.Data.Hibernate
{
    using Framework.Modelo;
    using Veros.Data;
    using Veros.Framework;
    using NHibernate;

    /// <summary>
    /// Base para repositórios singleton utilizando NHibernate
    /// </summary>
    /// <typeparam name="T">Tipo da entidade</typeparam>
    public abstract class RepositorioUnico<T> : 
        IRepositorioUnico<T> where T : EntidadeUnica, new()
    {
        protected ISessionBuilder sessionBuilder;
        protected IUnitOfWork unitOfWork;

        protected RepositorioUnico()
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

        /// <summary>
        /// Salva uma entidade no banco de dados
        /// </summary>
        /// <param name="item">Tipo da entidade</param>
        public virtual void Salvar(T item)
        {
            this.ApagarTodos();
            this.Session.Save(item);
        }

        /// <summary>
        /// Busca uma entidade no banco pelo id
        /// </summary>
        /// <returns>Retorna uma Entidade</returns>
        public virtual T Obter()
        {
            var singleton = this.Session.CreateQuery("from " + typeof(T).Name)
                .SetMaxResults(1)
                .SetCacheable(this.PodeFazerCache())
                .UniqueResult<T>();

            return singleton ?? new T();
        }

        /// <summary>
        /// Apaga todos os registros de um certo tipo
        /// </summary>
        public virtual void ApagarTodos()
        {
            this.Session.Delete("from " + typeof(T).Name);
        }

        public virtual bool PodeFazerCache()
        {
            return false;
        }
    }
}
