namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031625)]
    public class CriaRegraProcHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("REGRAPROC_HIST") == false)
            {
                var sql = @"CREATE TABLE REGRAPROC_HIST AS (SELECT * FROM REGRAPROC_BK  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("REGRAPROC_HIST"))
            {
                var sql = @"DROP TABLE REGRAPROC_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
