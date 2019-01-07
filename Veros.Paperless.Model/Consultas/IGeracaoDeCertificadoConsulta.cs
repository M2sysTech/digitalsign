namespace Veros.Paperless.Model.Consultas
{
    using System.Collections.Generic;

    public interface IGeracaoDeCertificadoConsulta
    {
        IList<CertificadoDeEntregaConsulta> Obter();
    }
}
