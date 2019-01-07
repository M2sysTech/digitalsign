namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201606151534)]
    public class AdicionaPh3OkNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", "BATCH_PH3OK", DbType.Boolean);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("BATCH", "BATCH_PH3OK");
        }
    }
}
