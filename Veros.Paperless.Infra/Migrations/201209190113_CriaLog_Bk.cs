namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190113)]
    public class CriaLogBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOG_BK",
                this.WithId("LOG_CODE"),
                new Column("LOG_DATETIME", DbType.DateTime),
                new Column("USR_NAME", DbType.AnsiString, 127),
                new Column("LOG_MODULE", DbType.AnsiString, 127),
                new Column("LOG_ACTION", DbType.AnsiString, 3),
                new Column("LOG_REGISTER", DbType.Int32),
                new Column("LOG_DESC", DbType.AnsiString, 254),
                new Column("LOG_STATION", DbType.AnsiString, 127),
                new Column("LOG_CHECKED", DbType.AnsiString, 1),
                new Column("LOG_USRCHECK", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_LOG_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOG_BK");
            this.Database.RemoveSequence("SEQ_LOG_BK");
        }
    }
}
