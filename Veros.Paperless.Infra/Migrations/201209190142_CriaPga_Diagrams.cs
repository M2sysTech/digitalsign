namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190142)]
    public class CriaPgaDiagrams : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PGA_DIAGRAMS",
                this.WithId("PGA_CODE"),
                new Column("DIAGRAMTABLES", DbType.AnsiString, 4000),
                new Column("DIAGRAMLINKS", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_PGA_DIAGRAMS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PGA_DIAGRAMS");
            this.Database.RemoveSequence("SEQ_PGA_DIAGRAMS");
        }
    }
}
