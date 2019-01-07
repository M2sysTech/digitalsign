namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190112)]
    public class CriaLogsvrBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGSVR_BK",
                this.WithId("LOGSVR_CODE"),
                new Column("LOGSVR_DATETIME", DbType.DateTime, ColumnProperty.NotNull),
                new Column("LOGSVR_SERVER", DbType.AnsiString, 1),
                new Column("LOGSVR_ERRORDESC", DbType.AnsiString, 254),
                new Column("LOGSVR_CHECKED", DbType.AnsiString, 1),
                new Column("LOGSVR_USRCHECK", DbType.AnsiString, 16));

            this.Database.CreateSequence("SEQ_LOGSVR_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGSVR_BK");
            this.Database.RemoveSequence("SEQ_LOGSVR_BK");
        }
    }
}
