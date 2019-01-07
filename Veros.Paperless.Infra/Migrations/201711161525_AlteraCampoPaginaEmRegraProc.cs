namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201711161525)]
    public class AlteraCampoPaginaEmRegraProc : Migration
    {
        public override void Up()
        {
            this.Database.ExecuteNonQuery("ALTER TABLE REGRAPROC ADD REGRAPROC_PAGINA1 VARCHAR2(100) NULL");
            this.Database.ExecuteNonQuery("UPDATE REGRAPROC SET REGRAPROC_PAGINA1 = REGRAPROC_PAGINA");
            this.Database.ExecuteNonQuery("ALTER TABLE REGRAPROC DROP COLUMN REGRAPROC_PAGINA");
            this.Database.ExecuteNonQuery("ALTER TABLE REGRAPROC RENAME COLUMN REGRAPROC_PAGINA1 to REGRAPROC_PAGINA");
        }

        public override void Down()
        {
        }
    }
}
