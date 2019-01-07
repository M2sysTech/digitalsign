namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190058)]
    public class CriaDraft : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "DRAFT",
                this.WithId("DRAFT_CODE"),
                new Column("CONTA", DbType.AnsiString, 12),
                new Column("BARCODE", DbType.AnsiString, 127),
                new Column("TIT", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_DRAFT");
        }

        public override void Down()
        {
            this.Database.RemoveTable("DRAFT");
            this.Database.RemoveSequence("SEQ_DRAFT");
        }
    }
}
