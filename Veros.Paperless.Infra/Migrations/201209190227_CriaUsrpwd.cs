namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190227)]
    public class CriaUsrpwd : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "USRPWD",
                this.WithId("USRPWD_CODE"),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("USRPWD_HASHPWD", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("USRPWD_LASTDT", DbType.DateTime));

            this.Database.CreateSequence("SEQ_USRPWD");
        }

        public override void Down()
        {
            this.Database.RemoveTable("USRPWD");
            this.Database.RemoveSequence("SEQ_USRPWD");
        }
    }
}
