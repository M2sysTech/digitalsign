namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201709211045)]
    public class AlteraCampoNomeArquivoEmColeta : Migration
    {
        public override void Up()
        {
            this.Database.ExecuteNonQuery("ALTER TABLE COLETA MODIFY COLETA_ARQUIVO VARCHAR2(300)");
        }

        public override void Down()
        {
        }
    }
}
