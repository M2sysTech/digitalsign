namespace Veros.Paperless.Model.Servicos
{
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;

    public interface ICertificadoA3
    {
        X509Certificate2 Obter();

        ICollection<Org.BouncyCastle.X509.X509Certificate> CadeiaCertificadoTempo();
    }
}