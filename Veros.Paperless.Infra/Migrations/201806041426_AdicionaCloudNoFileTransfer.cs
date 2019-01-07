namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201806041426)]
    public class AdicionaCloudNoFileTransfer : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("FILETRANSFER", new Column("FILETRANSFER_CLOUD", DbType.Boolean));
        }

        public override void Down()
        {
        }
    }
}
