namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306041120)]
    public class AdicionarIntervaloDeExclusaoDefinitivaHistorico : Migration
    {
        public override void Up()
        {
            var sql = @"ALTER TABLE EXPURGOCONFIG ADD (EXPURGOCONFIG_INTERVALOEXCL NUMBER(3))";
            
            this.Database.ExecuteNonQuery(sql);
        }

        public override void Down()
        {
            var sql = @"ALTER TABLE EXPURGOCONFIG DROP COLUMN EXPURGOCONFIG_INTERVALOEXCL";

            this.Database.ExecuteNonQuery(sql);
        }
    }
}
