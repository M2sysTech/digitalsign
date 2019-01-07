namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190149)]
    public class CriaPgaScripts : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PGA_SCRIPTS",
                this.WithId("PGA_CODE"),
                new Column("SCRIPTSOURCE", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_PGA_SCRIPTS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PGA_SCRIPTS");
            this.Database.RemoveSequence("SEQ_PGA_SCRIPTS");
        }
    }
}
