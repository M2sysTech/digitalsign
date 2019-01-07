namespace Veros.Framework.Security
{
    using System;
    using System.IO;
    using System.Text;
    using System.Security.Cryptography;

    public class SimplerAes : ICryptography
    {
        private static readonly byte[] key = { 122, 55, 19, 11, 24, 155, 85, 45, 114, 204, 27, 162, 37, 112, 222, 10, 241, 24, 175, 144, 1, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
        private static readonly byte[] vector = { 210, 64, 191, 111, 23, 3, 132, 119, 231, 55, 221, 112, 79, 238, 114, 156 };
        private readonly ICryptoTransform encryptor;
        private readonly ICryptoTransform decryptor;
        private readonly UTF8Encoding encoder;

        public SimplerAes()
        {
            var rm = new RijndaelManaged();
            this.encryptor = rm.CreateEncryptor(key, vector);
            this.decryptor = rm.CreateDecryptor(key, vector);
            this.encoder = new UTF8Encoding();
        }

        public string Encode(string value)
        {
            return Convert.ToBase64String(this.Encrypt(this.encoder.GetBytes(value)));
        }

        public string Decode(string value)
        {
            return this.encoder.GetString(this.Decrypt(Convert.FromBase64String(value)));
        }

        ////private string EncryptToUrl(string unencrypted)
        ////{
        ////    return HttpUtility.UrlEncode(Encrypt(unencrypted));
        ////}

        ////private string DecryptFromUrl(string encrypted)
        ////{
        ////    return Decrypt(HttpUtility.UrlDecode(encrypted));
        ////}

        private byte[] Encrypt(byte[] buffer)
        {
            return this.Transform(buffer, this.encryptor);
        }

        private byte[] Decrypt(byte[] buffer)
        {
            return this.Transform(buffer, this.decryptor);
        }

        private byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            var stream = new MemoryStream();
            
            using (var cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }

            return stream.ToArray();
        }
    }
}
