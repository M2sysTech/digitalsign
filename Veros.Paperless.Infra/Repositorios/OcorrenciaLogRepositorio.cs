namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class OcorrenciaLogRepositorio : Repositorio<OcorrenciaLog>, IOcorrenciaLogRepositorio
    {
        public IList<OcorrenciaLog> ObterPorOcorrencia(int ocorrenciaId)
        {
            return this.Session.QueryOver<OcorrenciaLog>()
                .Where(x => x.Ocorrencia.Id == ocorrenciaId)
                .List();
        }

        public IList<OcorrenciaLog> ObterPorDocumentoEtipo(int documentoId, int codTipoOcorrencia)
        {
            return this.Session.QueryOver<OcorrenciaLog>()
                .JoinQueryOver(x => x.Ocorrencia)
                .Where(x => x.Tipo.Id == codTipoOcorrencia)
                .And(x => x.Documento.Id == documentoId)
                .List();
        }
    }
}