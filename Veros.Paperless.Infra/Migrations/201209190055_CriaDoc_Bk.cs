namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190055)]
    public class CriaDocBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "DOC_BK",
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
                new Column("DOC_INFO", DbType.AnsiString, 127),
                new Column("DOC_AGENCIAREMET", DbType.AnsiString, 4),
                new Column("DOC_FILESIZEF", DbType.Int32),
                new Column("DOC_FILESIZEV", DbType.Int32),
                new Column("DOC_ORDER", DbType.Int32),
                new Column("DOC_PAGMDOC", DbType.Int32),
                new Column("DOC_PAGBATCH", DbType.Int32),
                new Column("DOC_RECAPTURADO", DbType.AnsiString, 1),
                new Column("DOC_FILETYPEV", DbType.AnsiString, 16),
                new Column("DOC_HASHFJPG", DbType.AnsiString, 1024),
                new Column("DOC_HASHVJPG", DbType.AnsiString, 1024));

            this.Database.CreateSequence("SEQ_DOC_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("DOC_BK");
            this.Database.RemoveSequence("SEQ_DOC_BK");
        }
    }
}
