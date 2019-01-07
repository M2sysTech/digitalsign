namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031537)]
    public class CriaLogMdocHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("LOGMDOC_BK") == false)
            {
                var sql = @"CREATE TABLE LOGMDOC_HIST AS (SELECT * FROM LOGMDOC_BK  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("LOGMDOC_BK"))
            {
                var sql = @"DROP TABLE LOGMDOC_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
