namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031420)]
    public class CriaRegraProcBkCasoNaoExista : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("REGRAPROC_BK") == false)
            {
                var sql = @"CREATE TABLE REGRAPROC_BK AS (SELECT * FROM REGRAPROC WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("REGRAPROC_BK"))
            {
                var sql = @"DROP TABLE REGRAPROC_BK";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
