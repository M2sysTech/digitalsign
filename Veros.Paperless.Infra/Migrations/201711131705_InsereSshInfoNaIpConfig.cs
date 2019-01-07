namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201711131705)]
    public class InsereSshInfoNaIpConfig : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("IPCONFIG", new Column("IPCONFIG_PRIVATEKEY", DbType.AnsiString));
            this.Database.AddColumn("IPCONFIG", new Column("IPCONFIG_PASSPHRASE", DbType.AnsiString));
            this.Database.AddColumn("IPCONFIG", new Column("IPCONFIG_REMOTEPATH", DbType.AnsiString));
            this.Database.AddColumn("IPCONFIG", new Column("IPCONFIG_PWDSSH", DbType.AnsiString));
            this.Database.AddColumn("IPCONFIG", new Column("IPCONFIG_USRSSH", DbType.AnsiString));
            this.Database.AddColumn("IPCONFIG", new Column("IPCONFIG_KEYPAIRS", DbType.Boolean));
        }
       
        public override void Down()
        {
        }
    }
}