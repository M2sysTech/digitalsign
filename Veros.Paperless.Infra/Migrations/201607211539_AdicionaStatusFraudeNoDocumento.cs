namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201607211539)]
    public class AdicionaStatusFraudeNoDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDoc", new Column("MDoc_StatusFraude", DbType.AnsiString, 1));
            this.Database.AddColumn("MDoc_Bk", new Column("MDoc_StatusFraude", DbType.AnsiString, 1));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("MDoc", "MDoc_StatusFraude");
            this.Database.RemoveColumn("MDoc_Bk", "MDoc_StatusFraude");
        }
    }
}
