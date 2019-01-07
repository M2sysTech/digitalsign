namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201209241544)]
    public class AdicionaIndicioFraudeNoDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", new Column("MDOC_INDICIOFRAUDE", DbType.AnsiString, 200));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("MDOC", "MDOC_INDICIOFRAUDE");
        }
    }
}
