namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201706211000)]
    public class CriaTransportadora : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TRANSPORTADORA",
                this.WithId("TRANSP_CODE"),
                new Column("TRANSP_CNPJ", DbType.AnsiString, 20),
                new Column("TRANSP_NOME", DbType.AnsiString, 255));

            this.Database.CreateSequence("SEQ_TRANSP");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TRANSPORTADORA");
            this.Database.RemoveSequence("SEQ_TRANSP");
        }
    }
}
