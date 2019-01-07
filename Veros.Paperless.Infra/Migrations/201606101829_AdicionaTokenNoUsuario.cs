namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201606101829)]
    public class AdicionaTokenNoUsuario : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("USR", "USR_TOKEN", DbType.AnsiString, 1024);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("USR", "USR_TOKEN");
        }
    }
}
