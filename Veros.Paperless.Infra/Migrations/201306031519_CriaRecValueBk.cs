namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031519)]
    public class CriaRecValueBk : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("RECVALUE_BK") == false)
            {
                var sql = @"CREATE TABLE RECVALUE_BK AS (SELECT * FROM RECVALUE  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("RECVALUE_BK"))
            {
                var sql = @"DROP TABLE RECVALUE_BK";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
