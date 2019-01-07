namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201404291253)]
    public class AdicionaFlagRevisaoNaRegra : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REGRA", "REGRA_REVISAR", DbType.Boolean);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("REGRA", "REGRA_REVISAR");
        }
    }
}
