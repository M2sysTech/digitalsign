namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201705111758)]
    public class InsereIpConfigServicos : Migration
    {
        public override void Up()
        {
            this.AdicionarIpConfig("BATIMENTO", "192.168.10.80", 9995);
            this.AdicionarIpConfig("EXPORT", "192.168.10.80", 9996);
            this.AdicionarIpConfig("EXPURGO", "192.168.10.80", 9997);
            this.AdicionarIpConfig("FATURAMENTO", "192.168.10.80", 9998);
            this.AdicionarIpConfig("IMPORT", "192.168.10.80", 9999);
            this.AdicionarIpConfig("MONTAGEM", "192.168.10.80", 10001);
            this.AdicionarIpConfig("RECEPCAO", "192.168.10.80", 10002);
            this.AdicionarIpConfig("VALIDACAO", "192.168.10.80", 10003);
            this.AdicionarIpConfig("WORKFLOW", "192.168.10.80", 10004);
            this.AdicionarIpConfig("COMPLEMENTACAO", "192.168.10.80", 10005);
            this.AdicionarIpConfig("CLASSIFIER", "192.168.10.80", 10006);
        }

        public override void Down()
        {
        }

        private void AdicionarIpConfig(string servico, string ip, int porta)
        {
            var count = this.Database.ExecuteScalar("select count(*) from ipconfig where ipconfig_tag ='" + servico + "'").ToInt();

            var sql = string.Format(@"
insert into 
    ipconfig 
(ipconfig_code, ipconfig_dtcenterid, ipconfig_ip, ipconfig_port, ipconfig_tag)
    values
(seq_ipconfig.nextval, 1, '{0}', {1}, '{2}')",
                                              ip, 
                                              porta, 
                                              servico);
            if (count == 0)
            {
                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}