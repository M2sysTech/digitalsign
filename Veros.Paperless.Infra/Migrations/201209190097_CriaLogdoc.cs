namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190097)]
    public class CriaLogdoc : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGDOC",
                this.WithId("LOGDOC_CODE"),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("DOC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("LOGDOC_DATETIME", DbType.DateTime, ColumnProperty.NotNull),
                new Column("LOGDOC_ACTION", DbType.AnsiString, 3, ColumnProperty.NotNull),
                new Column("LOGDOC_VAL", DbType.Decimal),
                new Column("LOGDOC_OBS", DbType.AnsiString, 254),
                new Column("MDOC_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_LOGDOC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGDOC");
            this.Database.RemoveSequence("SEQ_LOGDOC");
        }
    }
}
