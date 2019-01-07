namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201511181120)]
    public class AdicionaUsuarioBrsafeNaUsr : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("USR", new Column("USR_LOGINBRSAFE", DbType.AnsiString, 200));
            this.Database.AddColumn("USR", new Column("USR_SENHABRSAFE", DbType.AnsiString, 200));
            this.Database.AddColumn("USRLOG", new Column("USR_LOGINBRSAFE", DbType.AnsiString, 200));
            this.Database.AddColumn("USRLOG", new Column("USR_SENHABRSAFE", DbType.AnsiString, 200));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("USR", "USR_LOGINBRSAFE");
            this.Database.RemoveColumn("USR", "USR_SENHABRSAFE");
            this.Database.RemoveColumn("USRLOG", "USR_LOGINBRSAFE");
            this.Database.RemoveColumn("USRLOG", "USR_SENHABRSAFE");
        }
    }
}