namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201807111112)]
    public class AdicionaDatasCloudNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("BATCH_PDFCLOUDEM", DbType.Date));
            this.Database.AddColumn("BATCH", new Column("BATCH_JPGCLOUDEM", DbType.Date));
            this.Database.AddColumn("BATCH", new Column("BATCH_FTREMOVIDOEM", DbType.Date));
        }

        public override void Down()
        {
        }
    }
}
