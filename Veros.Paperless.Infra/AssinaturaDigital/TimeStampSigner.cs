namespace Veros.Paperless.Infra.AssinaturaDigital
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using Org.BouncyCastle.Security;
    using iText.Signatures;
    using iText.Kernel.Pdf;
    using iText.Pdfa;
    using static iText.Signatures.PdfSigner;
    using iText.Kernel.Font;
    using Veros.Paperless.Model.Servicos;
    using Veros.Paperless.Model.Servicos.AssinaturaDigital;
    using Veros.Paperless.Infra.AssinaturaDigital;
    using Framework;
    using Framework.IO;

    public class TimeStampSigner : ITimeStampSigner
    {
        private const int EstimatedSize = 0;
        private readonly CertificadoA3 certificadoA3;
        private ICollection<Org.BouncyCastle.X509.X509Certificate> chain;
        private IOcspClient ocspClient;
        private ICollection<ICrlClient> crlList;
        private ITSAClient tsaClient;
        private ICollection<Org.BouncyCastle.X509.X509Certificate> cadeiaTempo;
        private IFileSystem fileSystem;

        public TimeStampSigner(CertificadoA3 certificadoA3, IFileSystem fileSystem)
        {
            this.certificadoA3 = certificadoA3;
        }

        public string Execute(SingInfo singInfo)
        {
            var inicioCertificado = DateTime.Now;

            var certificate = this.certificadoA3.Obter();
            this.cadeiaTempo = this.certificadoA3.CadeiaCertificadoTempo();

            this.MontarEstruturaCertificacao(certificate);

            var fimCertificado = DateTime.Now;

            var pdfAux = Path.Combine(
                Path.GetDirectoryName(singInfo.SourcePdf),
                Path.GetFileNameWithoutExtension(singInfo.SourcePdf) + "-aux.pdf");

            var inicioAssinatura = DateTime.Now;
            using (var reader = new PdfReader(singInfo.SourcePdf))
            {
                var stamping = new StampingProperties();

                stamping.UseAppendMode();

                var pdfSigner = new PdfSigner(reader, new FileStream(pdfAux, FileMode.Create), stamping);

                var caminhoFonte2 = @"C:\Windows\Fonts\cour.ttf";

                PdfSignatureAppearance appearance = pdfSigner
                    .GetSignatureAppearance()
                    .SetReason(singInfo.SignatureReason)
                    .SetLocation(singInfo.SignatureLocation)
                    .SetReuseAppearance(false)
                    .SetContact(singInfo.SignatureContact)
                    .SetLayer2Font(PdfFontFactory.CreateFont(caminhoFonte2, iText.IO.Font.PdfEncodings.WINANSI, true));

                appearance.SetRenderingMode(PdfSignatureAppearance.RenderingMode.DESCRIPTION);

                var pks = new X509Certificate2Signature(certificate, DigestAlgorithms.SHA256);

                Console.WriteLine("assinando...");

                pdfSigner.SignDetached(
                    pks,
                    this.chain.ToArray(),
                    this.crlList,
                    this.ocspClient,
                    this.tsaClient,
                    EstimatedSize,
                    CryptoStandard.CMS);
            }

            var fimAssinatura = DateTime.Now;

            var tempoAssinatura = fimAssinatura.Subtract(inicioAssinatura);
            var tempoCertificado = fimCertificado.Subtract(inicioCertificado);

            Console.WriteLine("Tempo Assinatura: {0}ms", tempoAssinatura.TotalMilliseconds);
            Console.WriteLine("Tempo Certificado: {0}ms", tempoCertificado.TotalMilliseconds);

            this.AdicionarLtv(pdfAux, singInfo);

            this.RemoverArquivoTemporario(pdfAux);

            return singInfo.TargetPdf;
        }

        private void RemoverArquivoTemporario(string pdfAux)
        {
            try
            {
                if (this.fileSystem.Exists(pdfAux))
                {
                    this.fileSystem.DeleteFile(pdfAux);
                    Log.Application.Warn("Arquivo auxiliar removido com sucesso");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Nao consegui remover arquivo temporario. " + exception.Message);
            }
        }

        private void AdicionarLtv(string pdfPrimeiraAssinatura, SingInfo singInfo)
        {
            var inicioCarimbo = DateTime.Now;

            var stamping = new StampingProperties();

            stamping.UseAppendMode();

            PdfDocument pdfDoc = new PdfDocument(
                new PdfReader(pdfPrimeiraAssinatura), 
                new PdfWriter(singInfo.TargetPdf));

            LtvVerification v = new LtvVerification(pdfDoc);
            SignatureUtil signatureUtil = new SignatureUtil(pdfDoc);

            var names = signatureUtil.GetSignatureNames();
            var sigName = names[names.Count - 1];

            var pkcs7 = signatureUtil.VerifySignature(sigName);

            if (pkcs7.IsTsp())
            {
                v.AddVerification(
                    sigName,
                    this.ocspClient,
                    new CrlClientOnline(this.cadeiaTempo.ToArray()),
                    LtvVerification.CertificateOption.SIGNING_CERTIFICATE,
                    LtvVerification.Level.OCSP_CRL,
                    LtvVerification.CertificateInclusion.YES);
            }
            else
            {
                foreach (var name in names)
                {
                    v.AddVerification(
                        name,
                        this.ocspClient,
                        new CrlClientOnline(this.cadeiaTempo.ToArray()),
                        LtvVerification.CertificateOption.WHOLE_CHAIN,
                        LtvVerification.Level.OCSP_CRL,
                        LtvVerification.CertificateInclusion.NO);
                }
            }

            pdfDoc.Close();

            Console.WriteLine("Aplicando timestamp........");
            PdfReader r = new PdfReader(pdfPrimeiraAssinatura);
            PdfSigner pdfSigner = new PdfSigner(r, new FileStream(singInfo.TargetPdf, FileMode.Create), stamping);

            pdfSigner.Timestamp(this.tsaClient, null);

            var fimCarimbo = DateTime.Now;

            var tempoCarimbo = fimCarimbo.Subtract(inicioCarimbo);

            Console.WriteLine("Tempo Carimbo: {0}ms", tempoCarimbo.TotalMilliseconds);
        }

        private void MontarEstruturaCertificacao(X509Certificate2 certificate)
        {
            this.chain = this.GetChain(certificate);

            foreach (var cadeia in this.chain)
            {
                Console.WriteLine(cadeia.ToString());
            }

            Console.WriteLine("Conseguiu pegar valor da cadeia? " + this.chain != null);

            this.ocspClient = new OcspClientBouncyCastle(null);

            this.crlList = new List<ICrlClient>
            {
                new CrlClientOnline(this.chain.ToArray())
            };

            this.tsaClient = this.GetTsaClient(this.chain);

            Console.WriteLine("Conseguiu pegar valor da autoridade de tempo? " + this.tsaClient != null);
        }

        private ICollection<Org.BouncyCastle.X509.X509Certificate> GetChain(X509Certificate2 certificado)
        {
            Console.WriteLine("Obtendo cadeia de certificacao");

            var x509Chain = new X509Chain();
            x509Chain.Build(certificado);

            var cadeia = (from X509ChainElement x509ChainElement in x509Chain.ChainElements
                          select DotNetUtilities.FromX509Certificate(x509ChainElement.Certificate)).ToList();

            Console.WriteLine("Cadeia de certificação obtida com sucesso.");

            return cadeia;
        }

        private ITSAClient GetTsaClient(IEnumerable<Org.BouncyCastle.X509.X509Certificate> chain)
        {
            Console.WriteLine("Obtendo TimeStamp Authority do certificado");

            ////https://freetsa.org/tsr

            return new CarimboBry(
                "https://act.bry.com.br",
                "36888977145",
                "M2sYs17B");
        }
    }
}