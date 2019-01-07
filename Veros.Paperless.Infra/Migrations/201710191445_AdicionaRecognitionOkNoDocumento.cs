namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201710191445)]
    public class AdicionaRecognitionOkNoDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", new Column("MDOC_RECOGNITIONEM", DbType.Date));
            this.Database.AddColumn("MDOC", new Column("MDOC_RECOGNITIONOK", DbType.Boolean));
        }

        public override void Down()
        {
        }
    }
}
