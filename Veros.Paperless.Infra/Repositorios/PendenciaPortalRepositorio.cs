namespace Veros.Paperless.Infra.Repositorios
{
    using Model.Entidades;
    using Model.Repositorios;
    using Veros.Data.Hibernate;

    public class PendenciaPortalRepositorio :
        Repositorio<PendenciaPortal>, IPendenciaPortalRepositorio
    {
        public PendenciaPortal ObterPorSolicitacaoId(int solicitacaoId)
        {
            return this.Session.QueryOver<PendenciaPortal>()
                .Where(x => x.SolicitacaoId == solicitacaoId)
                .Take(1)
                .SingleOrDefault();
        }
    }
}