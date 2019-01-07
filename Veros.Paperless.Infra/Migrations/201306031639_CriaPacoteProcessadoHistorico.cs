namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031639)]
    public class CriaPacoteProcessadoHistorico : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("PACOTEPROCESSADO_HIST") == false)
            {
                var sql = @"CREATE TABLE PACOTEPROCESSADO_HIST AS (SELECT * FROM PACOTEPROCESSADO_BK  WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("PACOTEPROCESSADO_HIST"))
            {
                var sql = @"DROP TABLE PACOTEPROCESSADO_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
