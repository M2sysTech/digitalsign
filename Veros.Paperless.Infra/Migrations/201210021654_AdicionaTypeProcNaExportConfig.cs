namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201210021654)]
    public class AdicionaTypeProcNaExportConfig : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("EXPORTCONFIG", new Column("TYPEPROC_CODE", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("EXPORTCONFIG", "TYPEPROC_CODE");
        }
    }
}