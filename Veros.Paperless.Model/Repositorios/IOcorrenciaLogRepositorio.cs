namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IOcorrenciaLogRepositorio : IRepositorio<OcorrenciaLog>
    {
        IList<OcorrenciaLog> ObterPorOcorrencia(int ocorrenciaId);

        IList<OcorrenciaLog> ObterPorDocumentoEtipo(int documentoId, int codTipoOcorrencia);
    }
}