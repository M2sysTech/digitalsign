namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190148)]
    public class CriaPgaReports : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PGA_REPORTS",
                this.WithId("PGA_CODE"),
                new Column("REPORTSOURCE", DbType.AnsiString, 4000),
                new Column("REPORTBODY", DbType.AnsiString, 4000),
                new Column("REPORTPROCS", DbType.AnsiString, 4000),
                new Column("REPORTOPTIONS", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_PGA_REPORTS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PGA_REPORTS");
            this.Database.RemoveSequence("SEQ_PGA_REPORTS");
        }
    }
}
