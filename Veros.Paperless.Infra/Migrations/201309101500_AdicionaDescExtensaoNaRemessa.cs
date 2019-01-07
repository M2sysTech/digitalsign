namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201309101500)]
    public class AdicionaDescExtensaoNaRemessa : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REMESSA", new Column("REMESSA_EXTENSAO", DbType.AnsiString, 4));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("REMESSA", "REMESSA_EXTENSAO");
        }
    }
}