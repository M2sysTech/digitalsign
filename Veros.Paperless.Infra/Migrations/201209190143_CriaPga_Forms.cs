namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190143)]
    public class CriaPgaForms : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PGA_FORMS",
                this.WithId("PGA_CODE"),
                new Column("FORMSOURCE", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_PGA_FORMS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PGA_FORMS");
            this.Database.RemoveSequence("SEQ_PGA_FORMS");
        }
    }
}
