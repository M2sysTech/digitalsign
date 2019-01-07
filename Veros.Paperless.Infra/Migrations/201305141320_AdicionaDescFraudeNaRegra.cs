namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201305141320)]
    public class AdicionaDescFraudeNaRegra : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REGRA", new Column("REGRA_DESCFRAUDE", DbType.AnsiString, 512));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("REGRA", "REGRA_DESCFRAUDE");
        }
    }
}
