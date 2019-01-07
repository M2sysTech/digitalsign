namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201708250800)]
    public class CriaCodigoBarrasCef : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CODIGOBARRASCEF",
                this.WithId("CODIGOBARRASCEF_CODE"),
                new Column("CODIGOBARRASCEF", DbType.AnsiString, 250),
                new Column("BATCH_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_CODIGOBARRASCEF");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CODIGOBARRASCEF");
            this.Database.RemoveSequence("SEQ_CODIGOBARRASCEF");
        }
    }
}
