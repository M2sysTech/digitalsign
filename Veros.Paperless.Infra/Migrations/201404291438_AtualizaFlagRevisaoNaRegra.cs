namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201404291438)]
    public class AtualizaFlagRevisaoNaRegra : Migration
    {
        public override void Up()
        {
            this.Database.ExecuteNonQuery("UPDATE REGRA SET REGRA_REVISAR = 0");
        }

        public override void Down()
        {
        }
    }
}