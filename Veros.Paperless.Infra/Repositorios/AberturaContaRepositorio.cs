namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class AberturaContaRepositorio : Repositorio<AberturaConta>, IAberturaContaRepositorio
    {
        public IList<AberturaConta> ObterPendentesImportacao()
        {
            return this.Session.QueryOver<AberturaConta>()
                .Where(x => x.Status == AberturaContaStatus.PendenteImportacao)
                .List();
        }

        public void FinalizarImportacao(int aberturaContaId)
        {
            this.Session
             .CreateQuery("update AberturaConta set Status = :status where Id = :id")
             .SetParameter("id", aberturaContaId)
             .SetParameter("status", AberturaContaStatus.Importado)
             .ExecuteUpdate();
        }
    }
}