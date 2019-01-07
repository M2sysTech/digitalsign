namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306101222)]
    public class CriaTabelaPackBkCasoNaoExista : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("PACK_BK") == false)
            {
                var sql = @"CREATE TABLE PACK_BK AS (SELECT * FROM PACK WHERE 1=2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("PACK_BK"))
            {
                var sql = @"DROP TABLE PACK_BK";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
