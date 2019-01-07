namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190132)]
    public class CriaOcr : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "OCR",
                this.WithId("OCR_CODE"),
                new Column("OCR_ENGINEA", DbType.AnsiString, 1),
                new Column("OCR_ENGINEB", DbType.AnsiString, 1),
                new Column("OCR_ENGINEC", DbType.AnsiString, 1),
                new Column("OCR_PREPROCES", DbType.Int32),
                new Column("OCR_MODE", DbType.Int32),
                new Column("OCR_ALTERZONE", DbType.AnsiString, 1),
                new Column("OCR_LANGUAGE", DbType.Int32),
                new Column("OCR_HEADER", DbType.AnsiString, 1),
                new Column("OCR_THRESHOLD", DbType.Int32),
                new Column("OCR_TRAINNING", DbType.AnsiString, 1),
                new Column("OCR_VERIFY", DbType.AnsiString, 1),
                new Column("OCR_MINPOINT", DbType.Int32),
                new Column("OCR_MAXPOINT", DbType.Int32));

            this.Database.CreateSequence("SEQ_OCR");
        }

        public override void Down()
        {
            this.Database.RemoveTable("OCR");
            this.Database.RemoveSequence("SEQ_OCR");
        }
    }
}
