namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306271358)]
    public class AdicionarDesfragmentacaoHoraNaExpurgoConfig : Migration
    {
        public override void Up()
        {
            var sql = @"ALTER TABLE EXPURGOCONFIG ADD (EXPURGOCONFIG_HORADESFRAG NUMBER(3))";

            this.Database.ExecuteNonQuery(sql);
        }

        public override void Down()
        {
            var sql = @"ALTER TABLE EXPURGOCONFIG DROP COLUMN EXPURGOCONFIG_HORADESFRAG";

            this.Database.ExecuteNonQuery(sql);
        }
    }
}
