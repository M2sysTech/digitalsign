namespace Veros.Paperless.Infra.Repositorios
{
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class TratamentoRegraRepositorio : Repositorio<TratamentoRegra>, ITratamentoRegraRepositorio
    {
        public TratamentoRegra ObterPorRegra(int regraId)
        {
            return this.Session.QueryOver<TratamentoRegra>()
                .Where(x => x.Regra.Id == regraId)
                .Take(1)
                .SingleOrDefault();
        }
    }
}