namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306031445)]
    public class CriaPacoteProcessadoBkCasoNaoExista : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("PACOTEPROCESSADO_BK") == false)
            {
                var sql = @"CREATE TABLE PACOTEPROCESSADO_BK AS (SELECT * FROM PACOTEPROCESSADO WHERE 1 = 2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("PACOTEPROCESSADO_BK"))
            {
                var sql = @"DROP TABLE PACOTEPROCESSADO_BK";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
