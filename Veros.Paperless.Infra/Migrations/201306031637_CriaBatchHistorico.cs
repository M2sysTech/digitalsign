namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031637)]
    public class CriaBatchHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("BATCH_HIST") == false)
            {
                var sql = @"CREATE TABLE BATCH_HIST AS (SELECT * FROM BATCH_BK  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("BATCH_HIST"))
            {
                var sql = @"DROP TABLE BATCH_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
