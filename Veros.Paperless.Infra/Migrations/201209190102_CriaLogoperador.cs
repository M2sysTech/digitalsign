namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190102)]
    public class CriaLogoperador : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGOPERADOR",
                this.WithId("LOGOPERADOR_CODE"),
                new Column("USR_NAME", DbType.AnsiString, 127),
                new Column("HR_LOGIN", DbType.DateTime),
                new Column("HR_LOGOFF", DbType.DateTime),
                new Column("STATION", DbType.AnsiString, 127),
                new Column("USR_MATRICULA", DbType.AnsiString, 127),
                new Column("USR_GROUP", DbType.Int32));

            this.Database.CreateSequence("SEQ_LOGOPERADOR");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGOPERADOR");
            this.Database.RemoveSequence("SEQ_LOGOPERADOR");
        }
    }
}
