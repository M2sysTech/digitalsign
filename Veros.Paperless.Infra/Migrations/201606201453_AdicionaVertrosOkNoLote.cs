namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201606201453)]
    public class AdicionaVertrosOkNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", "BATCH_VERTROSOK", DbType.Boolean);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("BATCH", "BATCH_VERTROSOK");
        }
    }
}
