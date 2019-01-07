namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031629)]
    public class CriaLogProcHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("LOGPROC_HIST") == false)
            {
                var sql = @"CREATE TABLE LOGPROC_HIST AS (SELECT * FROM LOGPROC_BK  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("LOGPROC_HIST"))
            {
                var sql = @"DROP TABLE LOGPROC_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
