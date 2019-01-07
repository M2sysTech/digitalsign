namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;

    public interface IRelatorioDeQuantitativoDeAssinaturaDigitalConsulta
    {
        IList<QuantitativoDeAssinaturaDigital> Obter(string dataInicio, string dataFim);
    }
}
