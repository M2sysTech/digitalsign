namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031400)]
    public class AdicionaCamposHistoricoExpurgoConfig : Migration
    {
        public override void Up()
        {
            var sql = @"ALTER TABLE EXPURGOCONFIG ADD (EXPURGOCONFIG_INTERVALODIASH NUMBER(3))";

            this.Database.ExecuteNonQuery(sql);

            sql = @"ALTER TABLE EXPURGOCONFIG ADD (EXPURGOCONFIG_ULTIMOEXPURGOH DATE)";

            this.Database.ExecuteNonQuery(sql);
        }

        public override void Down()
        {
            var sql = @"ALTER TABLE EXPURGOCONFIG DROP COLUMN EXPURGOCONFIG_INTERVALODIASH";

            this.Database.ExecuteNonQuery(sql);

            sql = @"ALTER TABLE EXPURGOCONFIG DROP COLUMN EXPURGOCONFIG_ULTIMOEXPURGOH";

            this.Database.ExecuteNonQuery(sql);
        }
    }
}
