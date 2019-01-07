namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031622)]
    public class CriaLogRemessaHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("LOGREMESSA_HIST") == false)
            {
                var sql = @"CREATE TABLE LOGREMESSA_HIST AS (SELECT * FROM LOGREMESSA_BK  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("LOGREMESSA_HIST"))
            {
                var sql = @"DROP TABLE LOGREMESSA_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
