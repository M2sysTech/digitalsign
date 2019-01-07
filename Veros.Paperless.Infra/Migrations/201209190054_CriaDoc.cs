namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190054)]
    public class CriaDoc : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "DOC",
                this.WithId("DOC_CODE"),
                new Column("BATCH_CODE", DbType.Int32),
                new Column("MDOC_CODE", DbType.Int32),
                new Column("DOC_DATE", DbType.DateTime),
                new Column("DOC_STATUS", DbType.AnsiString, 2),
                new Column("DOC_FILETYPE", DbType.AnsiString, 16),
                new Column("DOC_IMGF", DbType.AnsiString, 1),
                new Column("DOC_IMGV", DbType.AnsiString, 1),
                new Column("DOC_HASHF", DbType.AnsiString, 1024),
                new Column("DOC_HASHV", DbType.AnsiString, 1024),
                new Column("DOC_CRYPTO", DbType.AnsiString, 1),
                new Column("DOC_BANCO", DbType.AnsiString, 3),
                new Column("DOC_AGENCIA", DbType.AnsiString, 4),
                new Column("DOC_INFO", DbType.AnsiString, 1024),
                new Column("DOC_AGENCIAREMET", DbType.AnsiString, 4),
                new Column("DOC_FILESIZEF", DbType.Int32),
                new Column("DOC_FILESIZEV", DbType.Int32),
                new Column("DOC_ORDER", DbType.Int32),
                new Column("DOC_PAGMDOC", DbType.Int32),
                new Column("DOC_PAGBATCH", DbType.Int32),
                new Column("DOC_RECAPTURADO", DbType.AnsiString, 1),
                new Column("DOC_FILETYPEV", DbType.AnsiString, 16),
                new Column("DOC_BLANKPAGEF", DbType.AnsiString, 1),
                new Column("DOC_BLANKPAGEV", DbType.AnsiString, 1),
                new Column("DOC_TSOLJPGDIGIT", DbType.DateTime),
                new Column("DOC_TLIBSOLJPGDIGIT", DbType.DateTime),
                new Column("DOC_TSOLRECLASSIFIC", DbType.DateTime),
                new Column("DOC_TSOLLIBRECLASSIFIC", DbType.DateTime),
                new Column("DOC_TSOLJPGVALID", DbType.DateTime),
                new Column("DOC_TLIBSOLJPGVALID", DbType.DateTime),
                new Column("DOC_HASHFJPG", DbType.AnsiString, 1024),
                new Column("DOC_HASHVJPG", DbType.AnsiString, 1024),
                new Column("DOC_FILENAME", DbType.AnsiString, 50));

            this.Database.CreateSequence("SEQ_DOC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("DOC");
            this.Database.RemoveSequence("SEQ_DOC");
        }
    }
}
