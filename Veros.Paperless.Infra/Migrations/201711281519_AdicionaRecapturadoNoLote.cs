namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201711281519)]
    public class AdicionaRecapturadoNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("BATCH_RECAPTURADO", DbType.Boolean));
        }

        public override void Down()
        {
        }
    }
}
