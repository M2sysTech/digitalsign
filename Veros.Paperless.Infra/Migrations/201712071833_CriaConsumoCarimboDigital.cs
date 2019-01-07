namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201712071833)]
    public class CriaConsumoCarimboDigital : Migration
    {
        public override void Up()
        {
            this.Database.AddTable("CARIMBOCONSUMIDO",
                this.WithId("CARIMBOCONSUMIDO_CODE"),
                new Column("CARIMBOCONSUMIDO_EM", DbType.Date), 
                new Column("BATCH_CODE", DbType.Int32), 
                new Column("MDOC_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_CARIMBOCONSUMIDO");
        }

        public override void Down()
        {
        }
    }
}
