namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201506291325)]
    public class AdicionaDocVirtualNoDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", "MDOC_VIRTUAL", DbType.Int32);
            this.Database.AddColumn("MDOC_BK", "MDOC_VIRTUAL", DbType.Int32);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("MDOC", "MDOC_VIRTUAL");
            this.Database.RemoveColumn("MDOC_BK", "MDOC_VIRTUAL");
        }
    }
}
