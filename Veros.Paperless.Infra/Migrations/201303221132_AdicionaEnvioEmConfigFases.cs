namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201303221132)]
    public class AdicionaEnvioEmConfigFases : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn(
                "CONFIGFASES", 
                new Column("CONFIGFASES_ENVIO", DbType.AnsiString, 1));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("CONFIGFASES", "CONFIGFASES_ENVIO");
        }
    }
}