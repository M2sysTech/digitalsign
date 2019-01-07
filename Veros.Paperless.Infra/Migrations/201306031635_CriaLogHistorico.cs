namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031635)]
    public class CriaLogHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("LOG_HIST") == false)
            {
                var sql = @"CREATE TABLE LOG_HIST AS (SELECT * FROM LOG_BK  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("LOG_HIST"))
            {
                var sql = @"DROP TABLE LOG_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
