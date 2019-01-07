namespace Veros.Paperless.Infra.Storage
{
    using System;
    using System.Text;
    using Data;
    using Framework;
    using Model.Servicos.Complementacao;
    using Model.Storages;
    using Newtonsoft.Json;
    using BookSleeve;

    public class ConsultaVertrosStorage : IConsultaVertrosStorage
    {
        private RedisConnection redis = RedisConnectionGateway.Current.GetConnection();

        public VertrosStatus Obter(string cpf)
        {
            var key = this.GetKey(cpf);
            var item = this.redis.Strings.Get(0, key).Result;

            if (item == null)
            {
                return null;
            }

            var json = Encoding.Default.GetString(item);
            var vertrosStatus = JsonConvert.DeserializeObject<VertrosStatus>(json);

            return vertrosStatus;
        }

        public void Adicionar(string cpf, VertrosStatus vertrosStatus)
        {
            if (vertrosStatus == null)
            {
                Log.Application.Info("Não foram encontrados dados para o cpf " + cpf);
                return;
            }

            Log.Application.InfoFormat(
                "Adicionando Consulta Vertros {0} no redis em {1}:{2}",
                cpf,
                this.redis.Host,
                this.redis.Port);

            var key = this.GetKey(cpf);

            var json = JsonConvert.SerializeObject(vertrosStatus, Formatting.Indented);
            this.redis.Strings.Set(0, key, json);    
            
            this.redis.Keys.Expire(0, key, Convert.ToInt32(DateTime.Now.AddDays(1).TimeOfDay.TotalSeconds));
        }

        public void Apagar(string cpf)
        {
            this.redis.Keys.Remove(0, this.GetKey(cpf));
        }

        private string GetKey(string cpf)
        {
            return Database.Connection.Database + ":VertrosStatus:" + cpf;
        }
    }
}