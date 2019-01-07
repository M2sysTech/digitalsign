namespace Veros.Paperless.Model.Repositorios
{
    using Entidades;
    using Veros.Framework.Modelo;

    public interface IPendenciaPortalRepositorio : IRepositorio<PendenciaPortal>
    {
        PendenciaPortal ObterPorSolicitacaoId(int solicitacaoId);
    }
}