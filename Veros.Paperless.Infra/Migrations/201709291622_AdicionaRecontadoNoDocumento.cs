namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201709291622)]
    public class AdicionaRecontadoNoDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", new Column("MDOC_RECONTADO", DbType.Boolean));
        }

        public override void Down()
        {
        }
    }
}
