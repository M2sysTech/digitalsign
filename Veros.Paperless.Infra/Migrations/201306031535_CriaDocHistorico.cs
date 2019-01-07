namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031535)]
    public class CriaDocHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("DOC_HIST") == false)
            {
                var sql = @"CREATE TABLE DOC_HIST AS (SELECT * FROM DOC_BK  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("DOC_BK"))
            {
                var sql = @"DROP TABLE DOC_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
