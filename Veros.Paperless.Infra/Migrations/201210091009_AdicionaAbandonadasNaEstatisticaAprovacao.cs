namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201210091009)]
    public class AdicionaAbandonadasNaEstatisticaAprovacao : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("ESTATISTICAAPROVACAO", new Column("TOTAL_ABANDONADAS", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("ESTATISTICAAPROVACAO", "TOTAL_ABANDONADAS");
        }
    }
}