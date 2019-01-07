namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201212031942)]
    public class RemoveFaturamentoCodeDoLote : Migration
    {
        public override void Up()
        {
            this.Database.RemoveColumn("BATCH", "FATURAMENTO_CODE");
        }

        public override void Down()
        {
            this.Database.AddColumn("BATCH", new Column("FATURAMENTO_CODE", DbType.Int32));
        }
    }
}