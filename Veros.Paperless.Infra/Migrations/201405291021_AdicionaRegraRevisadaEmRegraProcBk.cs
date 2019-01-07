namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201405291021)]
    public class AdicionaRegraRevisadaEmRegraProcBk : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REGRAPROC_BK", "REGRAPROC_REVISADA", DbType.Boolean);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("REGRAPROC_BK", "REGRAPROC_REVISADA");
        }
    }
}
