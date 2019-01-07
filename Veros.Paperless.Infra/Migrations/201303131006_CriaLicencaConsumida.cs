namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201303131006)]
    public class CriaLicencaConsumida : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LICENCACONSUMIDA",
                this.WithId("LICENCACONSUMIDA_CODE"),
                new Column("DOC_CODE", DbType.Int32),
                new Column("LICENCACONSUMIDA_QTDE", DbType.Int32));
            this.Database.CreateSequence("SEQ_LICENCACONSUMIDA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LICENCACONSUMIDA");
            this.Database.RemoveSequence("SEQ_LICENCACONSUMIDA");
        }
    }
}