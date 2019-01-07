namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface ICamposReconhecidosPorDocumentoECampoConsulta
    {
        IList<CamposReconhecidosPorDocumentoECampo> Obter(DateTime dataInicio, DateTime dataFim);
    }
}
