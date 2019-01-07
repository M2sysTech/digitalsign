namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031440)]
    public class CriaRegraImpBkCasoNaoExista : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("REGRAIMP_BK") == false)
            {
                var sql = @"CREATE TABLE REGRAIMP_BK AS (SELECT * FROM REGRAIMP WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("REGRAIMP_BK"))
            {
                var sql = @"DROP TABLE REGRAIMP_BK";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
