namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031623)]
    public class CriaRemessaBk : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("REMESSA_BK") == false)
            {
                var sql = @"CREATE TABLE REMESSA_BK AS (SELECT * FROM REMESSA  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("REMESSA_BK"))
            {
                var sql = @"DROP TABLE REMESSA_BK";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
