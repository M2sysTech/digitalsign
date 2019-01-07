namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190111)]
    public class CriaLogsvr : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGSVR",
                this.WithId("LOGSVR_CODE"),
                new Column("LOGSVR_DATETIME", DbType.DateTime, ColumnProperty.NotNull),
                new Column("LOGSVR_SERVER", DbType.AnsiString, 1),
                new Column("LOGSVR_ERRORDESC", DbType.AnsiString, 254),
                new Column("LOGSVR_CHECKED", DbType.AnsiString, 1),
                new Column("LOGSVR_USRCHECK", DbType.AnsiString, 16));

            this.Database.CreateSequence("SEQ_LOGSVR");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGSVR");
            this.Database.RemoveSequence("SEQ_LOGSVR");
        }
    }
}
