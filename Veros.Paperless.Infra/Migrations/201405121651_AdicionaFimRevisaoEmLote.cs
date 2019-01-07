namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201405121651)]
    public class AdicionaFimRevisaoEmLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", "BATCH_TREVISAO", DbType.Date);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("BATCH", "BATCH_TREVISAO");
        }
    }
}
