namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Data.Hibernate;
    using Model.Entidades;
    using Model.Repositorios;

    public class RegraCondicionalRepositorio : Repositorio<RegraCondicional>, IRegraCondicionalRepositorio
    {
        public IList<RegraCondicional> ObterPorRegraId(int regraId)
        {
            return this.Session.QueryOver<RegraCondicional>()
                .Where(x => x.Regra.Id == regraId)
                .Fetch(x => x.TipoDocumento).Eager
                .OrderBy(x => x.Ordem).Asc
                .List<RegraCondicional>();
        }
    }
}
