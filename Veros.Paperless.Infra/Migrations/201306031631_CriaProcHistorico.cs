namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031631)]
    public class CriaProcHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("PROC_HIST") == false)
            {
                var sql = @"CREATE TABLE PROC_HIST AS (SELECT * FROM PROC_BK  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("PROC_HIST"))
            {
                var sql = @"DROP TABLE PROC_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
