namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201306101227)]
    public class CriaTabelaPackHistoricoCasoNaoExista : Migration
    {
        public override void Up()
        {
            if (this.Database.TableExists("PACK_HIST") == false)
            {
                var sql = @"CREATE TABLE PACK_HIST AS (SELECT * FROM PACK WHERE 1=2)";

                this.Database.ExecuteNonQuery(sql);
            }
        }

        public override void Down()
        {
            if (this.Database.TableExists("PACK_HIST"))
            {
                var sql = @"DROP TABLE PACK_HIST";

                this.Database.ExecuteNonQuery(sql);
            }
        }
    }
}
