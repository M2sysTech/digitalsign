namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;

    public interface ILicencaConsumidaPorTipoDocumentoConsulta
    {
        IList<LicencaConsumidaPorTipoDocumento> Obter(string dataInicio, string dataFim);
    }
}