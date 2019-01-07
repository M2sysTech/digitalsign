namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201305061627)]
    public class AdicionarCamposRestantesParaMdocDadosBk : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOCDADOS_BK", new Column("MDOCDADOS_ELEITO", DbType.Int32));
            this.Database.AddColumn("MDOCDADOS_BK", new Column("MDOCDADOS_OCRCOMPLEMENTOU", DbType.Boolean));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("MDOCDADOS_BK", "MDOCDADOS_ELEITO");
            this.Database.RemoveColumn("MDOCDADOS_BK", "MDOCDADOS_OCRCOMPLEMENTOU");
        }
    }
}
