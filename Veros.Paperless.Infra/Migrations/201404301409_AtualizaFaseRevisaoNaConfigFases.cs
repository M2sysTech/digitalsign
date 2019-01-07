namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201404301409)]
    public class AtualizaFaseRevisaoNaConfigFases : Migration
    {
        public override void Up()
        {
            this.Database.ExecuteNonQuery("UPDATE CONFIGFASES SET CONFIGFASES_REVISAO = 'N'");
        }

        public override void Down()
        {
        }
    }
}