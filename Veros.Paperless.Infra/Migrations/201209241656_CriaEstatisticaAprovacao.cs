namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209241656)]
    public class CriaEstatisticaAprovacao : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ESTATISTICAAPROVACAO",
                this.WithId("ESTATISTICAAPROVACAO_CODE"),
                new Column("USR_CODE", DbType.Int32),
                new Column("ESTATISTICAAPROVACAO_DATE", DbType.DateTime),
                new Column("TOTAL_DEVOLVIDAS", DbType.Int32),
                new Column("TOTAL_LIBERADAS", DbType.Int32),
                new Column("TOTAL_FRAUDE", DbType.Int32));

            this.Database.CreateSequence("SEQ_ESTATISTICAAPROVACAO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ESTATISTICAAPROVACAO");
            this.Database.RemoveSequence("SEQ_ESTATISTICAAPROVACAO");
        }
    }
}
