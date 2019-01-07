namespace Veros.Data.Hibernate
{
    using System.Collections.Generic;
    using Veros.Framework.Modelo;

    public class RepositorioReadOnly<T> : DaoBase, IRepositorioReadOnly<T> where T : Entidade
    {
        public virtual T ObterPorId(int id)
        {
            return this.Session.Get<T>(id);
        }

        public virtual T[] ObterTodos()
        {
            var items = this.Session
                .CreateQuery("from " + typeof(T).Name)
                .List<T>();

            return new List<T>(items).ToArray();
        }
    }
}
