namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201510291345)]
    public class AdicionarLoginBrSaeNoUsuario : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("USR", "USR_LOGINBRSAFE", DbType.String, 100);
            this.Database.AddColumn("USR", "USR_SENHABRSAFE", DbType.String, 100);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("USR", "USR_LOGINBRSAFE");
            this.Database.RemoveColumn("USR", "USR_SENHABRSAFE");
        }
    }
}
