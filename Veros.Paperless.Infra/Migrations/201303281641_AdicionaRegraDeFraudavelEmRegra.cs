namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201303281641)]
    public class AdicionaRegraDeFraudavelEmRegra : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REGRA", new Column("REGRA_FRAUDE", DbType.Boolean));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("REGRA", "REGRA_FRAUDE");
        }
    }
}