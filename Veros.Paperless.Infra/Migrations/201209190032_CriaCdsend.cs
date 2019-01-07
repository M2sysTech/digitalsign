namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190032)]
    public class CriaCdsend : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CDSEND",
                this.WithId("CDSEND_CODE"),
                new Column("CDSEND_NODE", DbType.AnsiString, 150, ColumnProperty.NotNull),
                new Column("CDSEND_USER", DbType.AnsiString, 150, ColumnProperty.NotNull),
                new Column("CDSEND_PWDS", DbType.AnsiString, 150, ColumnProperty.NotNull),
                new Column("CDSEND_REMOTEPATH", DbType.AnsiString, 150, ColumnProperty.NotNull),
                new Column("CDSEND_HOLD", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_CDSEND");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CDSEND");
            this.Database.RemoveSequence("SEQ_CDSEND");
        }
    }
}
