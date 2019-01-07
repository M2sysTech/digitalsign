namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201708041011)]
    public class AdicionaPaginaNaRegraViolada : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REGRAPROC", new Column("REGRAPROC_PAGINA", DbType.Int32));
            this.Database.AddColumn("REGRAPROC_BK", new Column("REGRAPROC_PAGINA", DbType.Int32));
            this.Database.AddColumn("REGRAPROC_HIST", new Column("REGRAPROC_PAGINA", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("REGRAPROC", "REGRAPROC_PAGINA");
            this.Database.RemoveColumn("REGRAPROC_BK", "REGRAPROC_PAGINA");
            this.Database.RemoveColumn("REGRAPROC_HIST", "REGRAPROC_PAGINA");
        }
    }
}
