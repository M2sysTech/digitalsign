namespace Veros.Paperless.Infra.Repositorios
{
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class RegraAcaoRepositorio : Repositorio<RegraAcao>, IRegraAcaoRepositorio
    {
        public RegraAcao ObterAcaoPorRegra(int regraId)
        {
            return this.Session.QueryOver<RegraAcao>()
                .Where(x => x.Regra.Id == regraId)
                .SingleOrDefault();
        }
    }
}