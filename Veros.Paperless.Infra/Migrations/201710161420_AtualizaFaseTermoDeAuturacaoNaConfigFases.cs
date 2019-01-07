namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201710161420)]
    public class AtualizaFaseTermoDeAuturacaoNaConfigFases : Migration
    {
        public override void Up()
        {
            this.Database.ExecuteNonQuery("UPDATE CONFIGFASES SET CONFIGFASES_TERMODEAUTUACAO = 'S'");
        }

        public override void Down()
        {
        }
    }
}