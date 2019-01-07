namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201807121642)]
    public class AdicionaDataFilaExportNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("BATCH_EXPTIME", DbType.Date));
        }

        public override void Down()
        {
        }
    }
}
