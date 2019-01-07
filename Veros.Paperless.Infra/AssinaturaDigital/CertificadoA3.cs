namespace Veros.Paperless.Infra.AssinaturaDigital
{
    using Framework;
    using Model.Servicos.AssinaturaDigital;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;

    public class CertificadoA3
    {
        public X509Certificate2 Obter()
        {
            var store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);

            if (store.Certificates.Count == 0)
            {
                throw new RepositorioCertificadosException("Certificados Locais do usuario indispon�veis.");
            }

            var cert = store.Certificates[0];

            Console.WriteLine("Certificado Selecionado {0} Serial {1} Subject {2}", cert.IssuerName.Name, cert.SerialNumber, cert.SubjectName);

            var rsa = (RSACryptoServiceProvider)cert.PrivateKey;

            var secureString = new SecureString();

            secureString.AppendChar(char.Parse("1"));
            secureString.AppendChar(char.Parse("2"));
            secureString.AppendChar(char.Parse("3"));
            secureString.AppendChar(char.Parse("4"));

            var cspp = new CspParameters();
            cspp.KeyContainerName = rsa.CspKeyContainerInfo.KeyContainerName;
            cspp.ProviderName = rsa.CspKeyContainerInfo.ProviderName;
            cspp.Flags = CspProviderFlags.UseUserProtectedKey;
            cspp.KeyPassword = secureString;

            cspp.ProviderType = rsa.CspKeyContainerInfo.ProviderType;

            cspp.Flags = CspProviderFlags.NoPrompt;

            var rsa2 = new RSACryptoServiceProvider(cspp);

            rsa.PersistKeyInCsp = true;

            store.Close();

            return cert;
        }

        public ICollection<Org.BouncyCastle.X509.X509Certificate> CadeiaCertificadoTempo()
        {
            var certificadoTempoArquivo = Path.Combine(Aplicacao.Caminho, "brycert.cer");
            var certificadoTempo = new X509Certificate2(certificadoTempoArquivo);
            var cp = new Org.BouncyCastle.X509.X509CertificateParser();

            var chain = new[]
            {
                cp.ReadCertificate(certificadoTempo.RawData)
            };

            Console.WriteLine("Cadeia de certifica��o obtida com sucesso.");

            return chain;
        }
    }
}