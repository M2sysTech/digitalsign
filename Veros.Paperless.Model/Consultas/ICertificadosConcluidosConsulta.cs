namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;

    public interface ICertificadosConcluidosConsulta
    {
        IList<CertificadoDeEntregaConsulta> Obter();
    }
}
