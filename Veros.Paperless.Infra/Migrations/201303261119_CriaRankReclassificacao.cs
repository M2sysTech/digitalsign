namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201303261119)]
    public class CriaRankReclassificacao : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "RANKRECLASSIFICACAO",
                this.WithId("RANKRECLASSIFICACAO_CODE"),
                new Column("TYPEDOC_ID", DbType.Int32),
                new Column("QTD", DbType.Int32));
            this.Database.CreateSequence("SEQ_RANKRECLASSIFICACAO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("RANKRECLASSIFICACAO");
            this.Database.RemoveSequence("SEQ_RANKRECLASSIFICACAO");
        }
    }
}