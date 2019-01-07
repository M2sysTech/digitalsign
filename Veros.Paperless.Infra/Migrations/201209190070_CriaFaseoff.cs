namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190070)]
    public class CriaFaseoff : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "FASEOFF",
                this.WithId("FASEOFF_CODE"),
                new Column("FASEOFF_BATCH", DbType.AnsiString, 4000),
                new Column("FASEOFF_PROC", DbType.AnsiString, 4000),
                new Column("FASEOFF_MDOC", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_FASEOFF");
        }

        public override void Down()
        {
            this.Database.RemoveTable("FASEOFF");
            this.Database.RemoveSequence("SEQ_FASEOFF");
        }
    }
}
