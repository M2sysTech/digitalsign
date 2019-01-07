namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class PendenciaColetaRepositorio : Repositorio<PendenciaColeta>, IPendenciaColetaRepositorio
    {
        public IList<PendenciaColeta> ObterPorArquivo(int arquivoColetaId)
        {
            return this.Session.QueryOver<PendenciaColeta>()
                .Where(x => x.ArquivoColeta.Id == arquivoColetaId)
                .List();
        }

        public void CancelarPorArquivo(ArquivoColeta arquivoColeta)
        {
            this.Session
              .CreateQuery("update PendenciaColeta set StatusDaPendencia = :statusNovo where ArquivoColeta.Id = :arquivoColetaId")
              .SetParameter("statusNovo", PendenciaColeta.StatusExcluida)
              .SetParameter("arquivoColetaId", arquivoColeta.Id)
              .ExecuteUpdate();
        }
    }
}