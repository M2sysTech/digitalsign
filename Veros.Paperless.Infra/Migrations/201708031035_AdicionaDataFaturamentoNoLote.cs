namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201708031035)]
    public class AdicionaDataFaturamentoNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("BATCH_TFATURAMENTO", DbType.Date));
            this.Database.AddColumn("BATCH_BK", new Column("BATCH_TFATURAMENTO", DbType.Date));
            this.Database.AddColumn("BATCH_HIST", new Column("BATCH_TFATURAMENTO", DbType.Date));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("BATCH", "BATCH_TFATURAMENTO");
            this.Database.RemoveColumn("BATCH_BK", "BATCH_TFATURAMENTO");
            this.Database.RemoveColumn("BATCH_HIST", "BATCH_TFATURAMENTO");
        }
    }
}
