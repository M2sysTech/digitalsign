namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201305211025)]
    public class AdicionaTipoOriginalNoDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", new Column("TYPEDOC_IDORIGINAL", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("MDOC", "TYPEDOC_IDORIGINAL");
        }
    }
}
