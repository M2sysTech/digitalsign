namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201710191502)]
    public class AdicionaRecognitionInicioNoDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", new Column("MDOC_STARTRECOGN", DbType.Date));
        }

        public override void Down()
        {
        }
    }
}
