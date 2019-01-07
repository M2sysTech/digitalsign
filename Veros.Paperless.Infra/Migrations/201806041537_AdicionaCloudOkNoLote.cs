namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201806041537)]
    public class AdicionaCloudOkNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("BATCH_COULDOK", DbType.Boolean));
            this.Database.AddColumn("BATCH", new Column("BATCH_JPGNOCLOUD", DbType.Boolean));
            this.Database.AddColumn("BATCH", new Column("BATCH_FTREMOVIDO", DbType.Boolean));
        }

        public override void Down()
        {
        }
    }
}
