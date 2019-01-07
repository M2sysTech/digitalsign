namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201708311011)]
    public class AdicionaQuantidadePaginasDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", new Column("MDOC_QTDEPAG", DbType.Int32));
        }

        public override void Down()
        {
        }
    }
}
