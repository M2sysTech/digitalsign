namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201305171120)]
    public class AdicionaReclassificadoNoDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", new Column("MDOC_RECLASSIFICADO", DbType.Boolean));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("MDOC", "MDOC_RECLASSIFICADO");
        }
    }
}