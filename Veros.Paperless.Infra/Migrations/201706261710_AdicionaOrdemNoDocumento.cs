namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201706261710)]
    public class AdicionaOrdemNoDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", new Column("MDOC_ORDEM", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("MDOC", "MDOC_ORDEM");
        }
    }
}
