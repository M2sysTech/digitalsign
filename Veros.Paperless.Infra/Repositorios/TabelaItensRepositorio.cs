namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class TabelaItensRepositorio : Repositorio<TabelaItens>, ITabelaItensRepositorio
    {
        public IList<TabelaItens> ObterPorIdentificador(string identificador)
        {
            return this.Session.QueryOver<TabelaItens>()
                .Where(x => x.Identificador == identificador)
                .List();
        }
    }
}
