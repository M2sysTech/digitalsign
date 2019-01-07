namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201710041000)]
    public class AdicionaBarcodeNoProcesso : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PROC", new Column("PROC_BARCODE", DbType.AnsiString, 100));
        }

        public override void Down()
        {
        }
    }
}
