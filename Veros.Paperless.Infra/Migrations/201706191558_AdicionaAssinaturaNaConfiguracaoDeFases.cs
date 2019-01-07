namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201706191558)]
    public class AdicionaAssinaturaNaConfiguracaoDeFases : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("CONFIGFASES", new Column("CONFIGFASES_ASSINATURA", DbType.AnsiString, 1));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("CONFIGFASES", "CONFIGFASES_ASSINATURA");
        }
    }
}
