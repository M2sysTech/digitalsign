namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201303072268)]
    public class AdicionaQtdeLimiteDescEmTypeDoc : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("TYPEDOC", new Column("TYPEDOC_QTDLIMITDESC", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("TYPEDOC", "TYPEDOC_QTDLIMITDESC");
        }
    }
}