namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201710031142)]
    public class AdicionaReavaliadoEmNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("BATCH_REAVALIADOEM", DbType.Date));
        }

        public override void Down()
        {
        }
    }
}
