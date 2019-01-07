namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class PalavraTipoRepositorio : Repositorio<PalavraTipo>, IPalavraTipoRepositorio
    {
        public IList<PalavraTipo> ObterTodosComTipo()
        {
            return this.Session.QueryOver<PalavraTipo>()
               .OrderBy(x => x.Id).Asc
               .JoinQueryOver(x => x.TipoDocumento)
               .Fetch(x => x.TipoDocumento).Eager
               .OrderBy(x => x.TypeDocCode).Asc
               .List<PalavraTipo>();
        }
    }
}
