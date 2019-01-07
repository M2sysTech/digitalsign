namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190101)]
    public class CriaLogmdocBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGMDOC_BK",
                this.WithId("LOGMDOC_CODE"),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("MDOC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("LOGMDOC_DATE", DbType.DateTime, ColumnProperty.NotNull),
                new Column("LOGMDOC_ACTION", DbType.AnsiString, 3, ColumnProperty.NotNull),
                new Column("LOGMDOC_OBS", DbType.AnsiString, 254));

            this.Database.CreateSequence("SEQ_LOGMDOC_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGMDOC_BK");
            this.Database.RemoveSequence("SEQ_LOGMDOC_BK");
        }
    }
}
