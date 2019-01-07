namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201710161400)]
    public class AdicionaFaseTermoDeAutuacaoNaConfigFases : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("CONFIGFASES", new Column("CONFIGFASES_TERMODEAUTUACAO", DbType.AnsiString, 1));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("CONFIGFASES", "CONFIGFASES_TERMODEAUTUACAO");
        }
    }
}