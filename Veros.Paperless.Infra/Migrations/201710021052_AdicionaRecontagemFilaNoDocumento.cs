namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201710021052)]
    public class AdicionaRecontagemFilaNoDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", new Column("MDOC_RECONTAGEMFILA", DbType.Date));
        }

        public override void Down()
        {
        }
    }
}
