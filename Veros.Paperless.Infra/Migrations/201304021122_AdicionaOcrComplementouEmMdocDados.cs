namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201304021122)]
    public class AdicionaOcrComplementouEmMdocDados : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOCDADOS", new Column("MDOCDADOS_OCRCOMPLEMENTOU", DbType.Boolean));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("MDOCDADOS", "MDOCDADOS_OCRCOMPLEMENTOU");
        }
    }
}