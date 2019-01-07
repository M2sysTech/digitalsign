namespace Veros.Paperless.Infra.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Data;
    using Framework;
    using Model.Storages;
    using Newtonsoft.Json;
    using Veros.Paperless.Model.Entidades;
    using BookSleeve;

    public class PalavraReconhecidaStorage : IPalavraReconhecidaStorage
    {
        private RedisConnection redis = RedisConnectionGateway.Current.GetConnection();

        public IList<PalavraReconhecida> Obter(int documentoId)
        {
            var key = this.GetKey(documentoId);
            var itemsCount = this.redis.Lists.GetLength(0, key).Result;
            var palavras = new List<PalavraReconhecida>();

            for (var i = 0; i < itemsCount; i++)
            {
                var bytes = this.redis.Lists.Get(0, key, i).Result;
                var json = Encoding.Default.GetString(bytes);
                var palavra = JsonConvert.DeserializeObject<PalavraReconhecida>(json);

                palavras.Add(palavra);
            }

            return palavras;
        }

        public void Adicionar(int documentoId, IList<PalavraReconhecida> palavrasReconhecidas)
        {
            Log.Application.InfoFormat(
                "Adicionando {0} palavras reconhecidas do documento {3} no redis em {1}:{2}",
                palavrasReconhecidas.Count,
                this.redis.Host,
                this.redis.Port, 
                documentoId);

            var key = this.GetKey(documentoId);

            Task task = null;
            foreach (var palavrasReconhecida in palavrasReconhecidas)
            {
                var json = JsonConvert.SerializeObject(palavrasReconhecida, Formatting.Indented);
                task = this.redis.Lists.AddLast(0, key, json);    
                ////this.redis.Keys.Expire(0, key, Convert.ToInt32(DateTime.Now.AddDays(1).TimeOfDay.TotalSeconds));
            }

            this.redis.Keys.Expire(0, key, Convert.ToInt32(DateTime.Now.AddDays(1).TimeOfDay.TotalSeconds));
        }

        public void Apagar(int documentoId)
        {
            this.redis.Keys.Remove(0, this.GetKey(documentoId));
        }

        private string GetKey(int documentoId)
        {
            return Database.Connection.Database + ":PalavrasReconhecidas:" + documentoId;
        }
    }
}