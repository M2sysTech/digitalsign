namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using NHibernate.Criterion;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class RegraRepositorio : Repositorio<Regra>, IRegraRepositorio
    {
        public Regra ObterRegraPorVinculo(string vinculo)
        {
            return this.Session.QueryOver<Regra>()
                .Where(x => x.Vinculo == vinculo)
                .Take(1)
                .SingleOrDefault();
        }

        public IList<Regra> ObterRegrasVinculadas(string vinculo)
        {
            return this.Session.QueryOver<Regra>()
                .Where(x => x.Vinculo == vinculo)
                .List<Regra>();
        }

        public IList<Regra> ObterRegrasPorFase(string fase, TipoProcesso tipoProcesso)
        {
            const string Query = @"
from 
    Regra regra
where
    regra.Ativada = :ativada and
    regra.Fase = :fase and
    (regra.TipoProcessoCode = :tipoProcessoCode or regra.TipoProcessoCode = 0 or regra.TipoProcessoCode is null)
order by regra.Id";

            return this.Session.CreateQuery(Query)
                .SetParameter("ativada", "S")
                .SetParameter("fase", fase)
                .SetParameter("tipoProcessoCode", tipoProcesso.Code)
                .SetCacheable(true)
                .SetCacheRegion("1d")
                .List<Regra>();
        }

        public Regra ObterRegraPorIdentificador(string identificador)
        {
            return this.Session.QueryOver<Regra>()
                .Where(x => x.Identificador == identificador)
                .Take(1)
                .SingleOrDefault();
        }
    }
}