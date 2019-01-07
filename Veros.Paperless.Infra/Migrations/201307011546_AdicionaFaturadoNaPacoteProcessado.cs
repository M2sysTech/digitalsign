namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201307011546)]
    public class AdicionaFaturadoNaPacoteProcessado : Migration
    {
        public override void Up()
        {
            var sql = @"ALTER TABLE PACOTEPROCESSADO ADD (FATURADO NUMBER(1))";

            this.Database.ExecuteNonQuery(sql);
        }

        public override void Down()
        {
            var sql = @"ALTER TABLE PACOTEPROCESSADO DROP COLUMN FATURADO";

            this.Database.ExecuteNonQuery(sql);
        }
    }
}
