namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201211290738)]
    public class AdicionaFraudesNaEstatisticaDeAprovacao : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("ESTATISTICAAPROVACAO", new Column("TOTAL_DEVOLVIDAS_FRAUDE", DbType.Int32));
            this.Database.AddColumn("ESTATISTICAAPROVACAO", new Column("TOTAL_LIBERADAS_FRAUDE", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("ESTATISTICAAPROVACAO", "TOTAL_DEVOLVIDAS_FRAUDE");
            this.Database.RemoveColumn("ESTATISTICAAPROVACAO", "TOTAL_LIBERADAS_FRAUDE");
        }
    }
}