namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190146)]
    public class CriaPgaLayout : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PGA_LAYOUT",
                this.WithId("PGA_CODE"),
                new Column("NRCOLS", DbType.Int32),
                new Column("COLNAMES", DbType.AnsiString, 4000),
                new Column("COLWIDTH", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_PGA_LAYOUT");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PGA_LAYOUT");
            this.Database.RemoveSequence("SEQ_PGA_LAYOUT");
        }
    }
}
