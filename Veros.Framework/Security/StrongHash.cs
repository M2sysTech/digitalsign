//-----------------------------------------------------------------------
// <copyright file="StrongHash.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Security
{
    using System.Security.Cryptography;
    using System.Text;

    public class StrongHash : IHash
    {
        private const string Key = "V3r0s1@a1b2c3d4e5f6g7h8i9j!l@m#n$o%p&";

        public string HashText(string value)
        {
            var bytes = Encoding.Default.GetBytes(value);
            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(Key));

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var transform = tdes.CreateEncryptor();
            var resultArray = transform.TransformFinalBlock(bytes, 0, bytes.Length);

            return Encoding.Default.GetString(resultArray);
        }

        public string HashFile(string file)
        {
            throw new System.NotImplementedException();
        }
    }
}
