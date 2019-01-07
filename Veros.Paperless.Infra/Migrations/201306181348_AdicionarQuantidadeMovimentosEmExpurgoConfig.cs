namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306181348)]
    public class AdicionarQuantidadeMovimentosEmExpurgoConfig : Migration
    {
        public override void Up()
        {
            var sql = @"ALTER TABLE EXPURGOCONFIG ADD (EXPURGOCONFIG_MOVIMENTOBASE NUMBER(3))";

            this.Database.ExecuteNonQuery(sql);
        }

        public override void Down()
        {
            var sql = @"ALTER TABLE EXPURGOCONFIG DROP COLUMN EXPURGOCONFIG_MOVIMENTOBASE";

            this.Database.ExecuteNonQuery(sql);
        }
    }
}
