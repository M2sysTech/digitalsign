namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306271630)]
    public class CriaTabelaAcompProdUsrHistCasoNaoExista : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("acompprodusr_hist") == false)
            {
                var sql = @"CREATE TABLE acompprodusr_hist AS (SELECT * FROM acompprodusr WHERE 1=2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("acompprodusr_hist"))
            {
                var sql = @"DROP TABLE acompprodusr_hist";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
