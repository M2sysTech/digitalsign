namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201302271453)]
    public class AdicionaRegraImpVinculoNaRegraViolada : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REGRAPROC", new Column("REGRAIMP_VINCULO", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("REGRAPROC", "REGRAIMP_VINCULO");
        }
    }
}