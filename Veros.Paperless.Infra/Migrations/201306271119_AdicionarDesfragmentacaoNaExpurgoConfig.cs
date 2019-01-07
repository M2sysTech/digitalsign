namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306271147)]
    public class AdicionarDesfragmentacaoNaExpurgoConfig : Migration
    {
        public override void Up()
        {
            var sql = @"ALTER TABLE EXPURGOCONFIG ADD (EXPURGOCONFIG_DIASDESFRAG NUMBER(3))";

            this.Database.ExecuteNonQuery(sql);
        }

        public override void Down()
        {
            var sql = @"ALTER EXPURGOCONFIG TABLE DROP COLUMN EXPURGOCONFIG_DIASDESFRAG";

            this.Database.ExecuteNonQuery(sql);
        }
    }
}
