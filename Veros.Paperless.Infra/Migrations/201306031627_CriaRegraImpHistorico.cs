namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031627)]
    public class CriaRegraImpHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("REGRAIMP_HIST") == false)
            {
                var sql = @"CREATE TABLE REGRAIMP_HIST AS (SELECT * FROM REGRAIMP_BK  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("REGRAIMP_HIST"))
            {
                var sql = @"DROP TABLE REGRAIMP_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
