namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201711231058)]
    public class AdicionaStartAjusteNoProcesso : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PROC", new Column("PROC_STARTAJUSTE", DbType.Date));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("PROC", "PROC_STARTAJUSTE");
        }
    }
}
