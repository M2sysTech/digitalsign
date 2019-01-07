namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190147)]
    public class CriaPgaQueries : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PGA_QUERIES",
                this.WithId("PGA_CODE"),
                new Column("QUERYTYPE", DbType.AnsiString, 1),
                new Column("QUERYCOMMAND", DbType.AnsiString, 4000),
                new Column("QUERYTABLES", DbType.AnsiString, 4000),
                new Column("QUERYLINKS", DbType.AnsiString, 4000),
                new Column("QUERYRESULTS", DbType.AnsiString, 4000),
                new Column("QUERYCOMMENTS", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_PGA_QUERIES");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PGA_QUERIES");
            this.Database.RemoveSequence("SEQ_PGA_QUERIES");
        }
    }
}
