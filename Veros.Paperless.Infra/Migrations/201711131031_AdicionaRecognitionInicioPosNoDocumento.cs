namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201711131031)]
    public class AdicionaRecognitionInicioPosNoDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", new Column("MDOC_STARTPOSRECOGN", DbType.Date));
            this.Database.AddColumn("MDOC", new Column("MDOC_RECOGNITIONPOSOK", DbType.Boolean));
            this.Database.AddColumn("MDOC", new Column("MDOC_RECOGNITIONPOSEM", DbType.Date));
        }

        public override void Down()
        {
        }
    }
}
