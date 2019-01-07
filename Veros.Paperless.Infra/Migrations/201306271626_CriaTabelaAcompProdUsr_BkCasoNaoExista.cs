namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306271626)]
    public class CriaTabelaAcompProdUsrBkCasoNaoExista : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("acompprodusr_bk") == false)
            {
                var sql = @"CREATE TABLE acompprodusr_bk AS (SELECT * FROM acompprodusr WHERE 1=2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("acompprodusr_bk"))
            {
                var sql = @"DROP TABLE acompprodusr_bk";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
