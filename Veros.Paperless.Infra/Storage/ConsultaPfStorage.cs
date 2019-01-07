namespace Veros.Paperless.Infra.Storage
{
    using System;
    using System.Text;
    using Data;
    using Framework;
    using Model.Ph3;
    using Model.Servicos.Batimento;
    using Model.Storages;
    using Newtonsoft.Json;
    using BookSleeve;

    public class ConsultaPfStorage : IConsultaPfStorage
    {
        private RedisConnection redis = RedisConnectionGateway.Current.GetConnection();

        public ConsultaPf Obter(string cpf)
        {
            cpf = cpf.RemoverDiacritico();

            var key = this.GetKey(cpf);
            var item = this.redis.Strings.Get(0, key).Result;

            if (item == null)
            {
                Log.Application.Warn("Nao foram encontrados dados no ConsultaPf para cpf #" + cpf);
                return null;
            }

            var json = Encoding.Default.GetString(item);
            var consultaPf = JsonConvert.DeserializeObject<ConsultaPf>(json);

            return consultaPf;
        }

        public void Adicionar(string cpf, ConsultaPf consultaPf)
        {
            if (consultaPf.DadosCadastrais == null)
            {
                Log.Application.Info("Não foram encontrados dados para o cpf " + cpf);
                return;
            }

            cpf = cpf.RemoverDiacritico();

            Log.Application.InfoFormat(
                "Adicionando Consulta Pessoa Fisica do cpf {0} no redis em {1}:{2}",
                cpf,
                this.redis.Host,
                this.redis.Port);

            var key = this.GetKey(cpf);

            var json = JsonConvert.SerializeObject(consultaPf, Formatting.Indented);
            this.redis.Strings.Set(0, key, json);    
            
            this.redis.Keys.Expire(0, key, Convert.ToInt32(DateTime.Now.AddDays(1).TimeOfDay.TotalSeconds));
        }

        public void Apagar(string cpf)
        {
            this.redis.Keys.Remove(0, this.GetKey(cpf));
        }

        private string GetKey(string cpf)
        {
            return Database.Connection.Database + ":ConsultaPf:" + cpf;
        }
    }
}