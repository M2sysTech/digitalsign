namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306271119)]
    public class AdicionarDesfragmentacaoDiasNaExpurgoConfig : Migration
    {
        public override void Up()
        {
            var sql = @"ALTER TABLE EXPURGOCONFIG ADD (EXPURGOCONFIG_ULTDESFRAGMENT DATE)";

            this.Database.ExecuteNonQuery(sql);
        }

        public override void Down()
        {
            var sql = @"ALTER TABLE EXPURGOCONFIG DROP COLUMN EXPURGOCONFIG_ULTDESFRAGMENT";

            this.Database.ExecuteNonQuery(sql);
        }
    }
}
