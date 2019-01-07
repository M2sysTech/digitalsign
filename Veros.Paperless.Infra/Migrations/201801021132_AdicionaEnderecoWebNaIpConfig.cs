namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201801021132)]
    public class AdicionaEnderecoWebNaIpConfig : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("IPCONFIG", new Column("IPCONFIG_URLWEB", DbType.AnsiString, 255));

            this.Database.ExecuteNonQuery("UPDATE IPCONFIG SET IPCONFIG_URLWEB = IPCONFIG_IP WHERE IPCONFIG_TAG LIKE 'FILE%'");
        }

        public override void Down()
        {
        }
    }
}
