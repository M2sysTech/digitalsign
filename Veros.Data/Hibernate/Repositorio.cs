namespace Veros.Data.Hibernate
{
    using System.Collections.Generic;
    using Framework.Modelo;
    using NHibernate;

    public abstract class Repositorio<T> : DaoBase, IRepositorio<T> where T : Entidade
    {
        public virtual void Salvar(T item)
        {
            this.Session.SaveOrUpdate(item);
        }

        public virtual void Apagar(T item)
        {
            this.Session.Delete(item);
        }

        public void ApagarPorId(int id)
        {
            this.Session.Delete("from " + typeof(T).Name + " where Id = " + id.ToString());
        }

        public virtual T ObterPorId(int id)
        {
            return this.Session.Get<T>(id);
        }

        public virtual T[] ObterTodos()
        {
            var items = this.Session.CreateQuery("from " + typeof(T).Name)
                .SetCacheable(this.DeveFazerCacheDoRepositorio())
                .List<T>();

            return new List<T>(items).ToArray();
        }

        public int Count()
        {
            IQuery query = this.Session.CreateQuery("select count(*) from " + typeof(T).Name);
            return (int) query.UniqueResult<long>();
        }

        public virtual void ApagarTodos()
        {
            this.Session.Delete("from " + typeof(T).Name);
        }

        public virtual bool DeveFazerCacheDoRepositorio()
        {
            return false;
        }
    }
}
