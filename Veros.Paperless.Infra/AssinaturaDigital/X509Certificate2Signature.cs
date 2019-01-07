using iText.Signatures;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Veros.Paperless.Infra.AssinaturaDigital
{
    public class X509Certificate2Signature : IExternalSignature
    {
        private string hashAlgorithm;
        private string encryptionAlgorithm;
        private X509Certificate2 certificate;

        public X509Certificate2Signature(X509Certificate2 certificate, string hashAlgorithm)
        {
            if (!certificate.HasPrivateKey)
            {
                throw new ArgumentException("No private key.");
            }

            this.certificate = certificate;
            this.hashAlgorithm = DigestAlgorithms.GetDigest(DigestAlgorithms.GetAllowedDigest(hashAlgorithm));

            if (certificate.PrivateKey is RSACryptoServiceProvider)
            {
                encryptionAlgorithm = "RSA";
            }
            else if (certificate.PrivateKey is DSACryptoServiceProvider)
            {
                encryptionAlgorithm = "DSA";
            }
            else
            {
                throw new ArgumentException("Unknown encryption algorithm " + certificate.PrivateKey);
            }
        }

        public virtual byte[] Sign(byte[] message)
        {
            if (certificate.PrivateKey is RSACryptoServiceProvider)
            {
                RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PrivateKey;
                return rsa.SignData(message, hashAlgorithm);
            }
            else
            {
                DSACryptoServiceProvider dsa = (DSACryptoServiceProvider)certificate.PrivateKey;
                return dsa.SignData(message);
            }
        }

        public virtual String GetHashAlgorithm()
        {
            return hashAlgorithm;
        }

        public virtual String GetEncryptionAlgorithm()
        {
            return encryptionAlgorithm;
        }
    }
}
