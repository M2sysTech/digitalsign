namespace Veros.Framework.Security
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public abstract class HashBase : IHash
    {
        public abstract string Format
        {
            get;
        }

        public string HashFile(string file)
        {
            using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                return this.GetHashFromStream(stream);
            }
        }

        public string HashText(string value)
        {
            using (var memoryStream = new MemoryStream(Encoding.Default.GetBytes(value.ToString())))
            {
                return this.GetHashFromStream(memoryStream);
            }
        }

        protected string ConvertToHexaString(byte[] hash)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString(this.Format));
            }

            return sb.ToString();
        }

        private string GetHashFromStream(Stream stream)
        {
            return this.ConvertToHexaString(MD5.Create().ComputeHash(stream));
        }
    }
}
