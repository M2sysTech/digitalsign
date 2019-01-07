namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190144)]
    public class CriaPgaGraphs : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PGA_GRAPHS",
                this.WithId("PGA_CODE"),
                new Column("GRAPHSOURCE", DbType.AnsiString, 4000),
                new Column("GRAPHCODE", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_PGA_GRAPHS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PGA_GRAPHS");
            this.Database.RemoveSequence("SEQ_PGA_GRAPHS");
        }
    }
}
