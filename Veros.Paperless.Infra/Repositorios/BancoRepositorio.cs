namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class BancoRepositorio : Repositorio<Banco>, IBancoRepositorio
    {
        public IList<Banco> ObterTodosPorOrdemDeNome()
        {
            return this.Session.QueryOver<Banco>().OrderBy(x => x.Nome).Desc.List();
        }
    }
}