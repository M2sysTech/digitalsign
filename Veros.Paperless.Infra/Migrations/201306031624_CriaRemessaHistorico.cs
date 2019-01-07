namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031624)]
    public class CriaRemessaHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("REMESSA_HIST") == false)
            {
                var sql = @"CREATE TABLE REMESSA_HIST AS (SELECT * FROM REMESSA_BK  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("REMESSA_HIST"))
            {
                var sql = @"DROP TABLE REMESSA_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
