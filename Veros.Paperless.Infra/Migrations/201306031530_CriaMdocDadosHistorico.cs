namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031530)]
    public class CriaMdocDadosHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("MDOCDADOS_HIST") == false)
            {
                var sql = @"CREATE TABLE MDOCDADOS_HIST AS (SELECT * FROM MDOCDADOS_BK  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("MDOCDADOS_HIST"))
            {
                var sql = @"DROP TABLE MDOCDADOS_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
