namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190098)]
    public class CriaLogdocBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGDOC_BK",
                this.WithId("LOGDOC_CODE"),
                new Column("USR_CODE", DbType.Int32),
                new Column("DOC_CODE", DbType.Int32),
                new Column("LOGDOC_DATETIME", DbType.DateTime),
                new Column("LOGDOC_ACTION", DbType.AnsiString, 3),
                new Column("LOGDOC_VAL", DbType.Decimal),
                new Column("LOGDOC_OBS", DbType.AnsiString, 254),
                new Column("MDOC_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_LOGDOC_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGDOC_BK");
            this.Database.RemoveSequence("SEQ_LOGDOC_BK");
        }
    }
}
