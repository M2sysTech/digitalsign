namespace Veros.Paperless.Infra.Repositorios
{
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class ListaNegraCpfRepositorio : Repositorio<ListaNegraCpf>, IListaNegraCpfRepositorio
    {
        public bool CpfEstaNaListaNegra(string cpf)
        {
            return this.Session.QueryOver<ListaNegraCpf>()
                .Where(x => x.Numero == cpf)
                .List().Count > 0;
        }
    }
}
