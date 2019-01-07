namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class DominioCampoRepositorio : Repositorio<DominioCampo>, IDominioCampoRepositorio
    {
        public DominioCampo ObterPorCodigoEChave(string codigo, string chave)
        {
            return this.Session.QueryOver<DominioCampo>()
                .Where(x => x.Codigo == codigo)
                .And(x => x.Chave == chave)
                .SingleOrDefault();
        }

        public IList<DominioCampo> ObterDominiosPorCodigo(string codigo)
        {
            return this.Session.QueryOver<DominioCampo>()
                .Where(x => x.Codigo == codigo)
                .OrderBy(x => x.Chave).Asc
                .List();
        }
    }
}
