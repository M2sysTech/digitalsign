namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201606011530)]
    public class AdicionaFuncaoCompararNaRegraCondicao : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REGRACOND", "REGRACOND_FUNCAOCOMPARAR", DbType.AnsiString, 200);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("REGRACOND", "REGRACOND_FUNCAOCOMPARAR");
        }
    }
}
