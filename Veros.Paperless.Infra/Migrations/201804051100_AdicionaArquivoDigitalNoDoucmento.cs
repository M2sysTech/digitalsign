namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201804051100)]
    public class AdicionaArquivoDigitalNoDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", new Column("MDOC_ARQUIVODIGITAL", DbType.Int32));
        }

        public override void Down()
        {
        }
    }
}
