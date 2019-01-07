namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031520)]
    public class CriaRecValueHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("RECVALUE_HIST") == false)
            {
                var sql = @"CREATE TABLE RECVALUE_HIST AS (SELECT * FROM RECVALUE_BK WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("RECVALUE_HIST"))
            {
                var sql = @"DROP TABLE RECVALUE_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
