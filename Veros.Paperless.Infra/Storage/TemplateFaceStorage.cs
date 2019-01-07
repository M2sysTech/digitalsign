namespace Veros.Paperless.Infra.Storage
{
    using System.Text;
    using System.Threading.Tasks;
    using BookSleeve;
    using Veros.Data;
    using Veros.Framework;

    public class TemplateFaceStorage 
    {
        private RedisConnection redis = RedisConnectionGateway.Current.GetConnection();

        public string Obter(int paginaId)
        {
            var key = this.GetKey(paginaId);
            var result = this.redis.Strings.Get(1, key).Result;
            
            if (result == null)
            {
                return null;
            }

            var stringHexa = Encoding.Default.GetString(result);
            return stringHexa;
        }

        public void Adicionar(int paginaId, byte[] templateFace)
        {
            Log.Application.InfoFormat(
                "Adicionando template face reconhecido do doc_code {0} no redis em {1}:{2}",
                paginaId,
                this.redis.Host,
                this.redis.Port);

            var key = this.GetKey(paginaId);
            
            Task task = null;
            task = this.redis.Sets.Add(1, key, templateFace);
        }

        public void Adicionar(int paginaId, string templateFace)
        {
            Log.Application.InfoFormat(
                "Adicionando template face reconhecido do doc_code {0} no redis em {1}:{2}",
                paginaId,
                this.redis.Host,
                this.redis.Port);

            var key = this.GetKey(paginaId);

            Task task = null;
            task = this.redis.Strings.Set(1, key, templateFace);
        }

        public string ByteArrayToString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        private string GetKey(int documentoId)
        {
            return Database.Connection.Database.ToLower() + ":TemplateFaces:" + documentoId;
        }
    }
}