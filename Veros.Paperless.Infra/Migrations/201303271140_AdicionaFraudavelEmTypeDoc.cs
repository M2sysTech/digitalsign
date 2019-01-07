namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201303271140)]
    public class AdicionaFraudavelEmTypeDoc : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("TYPEDOC", new Column("TYPEDOC_FRAUDAVEL", DbType.Boolean));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("TYPEDOC", "TYPEDOC_FRAUDAVEL");
        }
    }
}