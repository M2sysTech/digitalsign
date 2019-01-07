namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201211081750)]
    public class AdicionaFatoramentoCodeNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("FATURAMENTO_CODE", DbType.Int32 ));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("BATCH", "FATURAMENTO_CODE");
        }
    }
}