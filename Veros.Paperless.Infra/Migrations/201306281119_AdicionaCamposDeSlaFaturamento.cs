namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306281119)]
    public class AdicionaCamposDeSlaFaturamento : Migration
    {
        public override void Up()
        {
            var sql = @"ALTER TABLE FATURAMENTO ADD (SLAOK19HORAS NUMBER(4))";

            this.Database.ExecuteNonQuery(sql);

            sql = @"ALTER TABLE FATURAMENTO ADD (SLAOKAPOS17HORAS NUMBER(4))";

            this.Database.ExecuteNonQuery(sql);
        }

        public override void Down()
        {
            var sql = @"ALTER TABLE FATURAMENTO DROP COLUMN SLAOK19HORAS";

            this.Database.ExecuteNonQuery(sql);

            sql = @"ALTER TABLE FATURAMENTO DROP COLUMN SLAOKAPOS17HORAS";

            this.Database.ExecuteNonQuery(sql);
        }
    }
}
