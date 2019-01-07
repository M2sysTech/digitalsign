namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201606131638)]
    public class AdicionaUsuarioCapturaNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", "USR_CAPTURA", DbType.Int32);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("BATCH", "USR_CAPTURA");
        }
    }
}
