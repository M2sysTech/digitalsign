namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201211051413)]
    public class AdicionaDocumentoNaRegraProc : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REGRAPROC", new Column("MDOC_CODE", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("REGRAPROC", "MDOC_CODE");
        }
    }
}