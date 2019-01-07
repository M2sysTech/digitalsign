namespace Veros.Data.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201308011720)]
    public class AdicionaDataNoSeed : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("seedinfo", "appversion", DbType.String, 64);
            this.Database.AddColumn("seedinfo", "executed_at", DbType.DateTime);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("seedinfo", "appversion");
            this.Database.RemoveColumn("seedinfo", "executed_at");
        }
    }
}
