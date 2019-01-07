namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031633)]
    public class CriaLogBatchHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("LOGBATCH_HIST") == false)
            {
                var sql = @"CREATE TABLE LOGBATCH_HIST AS (SELECT * FROM LOGBATCH_BK  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("LOGBATCH_HIST"))
            {
                var sql = @"DROP TABLE LOGBATCH_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
