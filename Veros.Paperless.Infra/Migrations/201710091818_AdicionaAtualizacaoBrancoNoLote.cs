namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201710091818)]
    public class AdicionaAtualizacaoBrancoNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("BATCH_UPDBRANCO", DbType.Boolean));
        }

        public override void Down()
        {
        }
    }
}
