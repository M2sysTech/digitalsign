namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;

    public interface ICamposReconhecidosPorLoteConsulta
    {
        IList<CampoRecohecidoPorLote> Obter(string dataInicio, string dataFim);
    }
}
