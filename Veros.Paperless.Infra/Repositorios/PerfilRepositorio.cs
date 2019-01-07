namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class PerfilRepositorio : Repositorio<Perfil>, IPerfilRepositorio
    {
        public Perfil ObterPorSigla(string sigla)
        {
            return this.Session.QueryOver<Perfil>()
                .Where(x => x.Sigla == sigla)
                .SingleOrDefault<Perfil>();
        }

        public IList<Perfil> ObterPerfis()
        {
            return this.Session.QueryOver<Perfil>()
                .OrderBy(x => x.Descricao).Asc.List();
        }
    }
}