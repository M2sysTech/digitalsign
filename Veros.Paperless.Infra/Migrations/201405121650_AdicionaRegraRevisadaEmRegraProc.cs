namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201405121650)]
    public class AdicionaRegraRevisadaEmRegraProc : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REGRAPROC", "REGRAPROC_REVISADA", DbType.Boolean);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("REGRAPROC", "REGRAPROC_REVISADA");
        }
    }
}
