namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031533)]
    public class CriaLogDocHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("LOGDOC_BK") == false)
            {
                var sql = @"CREATE TABLE LOGDOC_HIST AS (SELECT * FROM LOGDOC_BK  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("LOGDOC_BK"))
            {
                var sql = @"DROP TABLE LOGDOC_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
