namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201212131105)]
    public class AdicionaFaturamentoStatus : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("FATURAMENTO", new Column("FATURAMENTO_STATUS", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("FATURAMENTO", "FATURAMENTO_STATUS");
        }
    }
}