namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201510201520)]
    public class AdicionaListaDeTipoDeDocumentoNaRegraTrat : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REGRATRAT", new Column("TYPEDOC_IDLISTCOMPARAR", DbType.String, 50));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("REGRATRAT", "TYPEDOC_IDLISTCOMPARAR");
        }
    }
}