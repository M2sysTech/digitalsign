namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201308141657)]
    public class AdicionaHoraUltimoArquivoRecebidoNaPacoteProcessado : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PACOTEPROCESSADO", new Column("PACOTEPROCESSADO_ULTIMOPACK", DbType.Date));

            var sql = @"ALTER TABLE PACOTEPROCESSADO_BK ADD (PACOTEPROCESSADO_ULTIMOPACK DATE)";

            this.Database.ExecuteNonQuery(sql);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("PACOTEPROCESSADO", "PACOTEPROCESSADO_ULTIMOPACK");

            var sql = @"ALTER TABLE PACOTEPROCESSADO DROP COLUMN PACOTEPROCESSADO_ULTIMOPACK";

            this.Database.ExecuteNonQuery(sql);
        }
    }
}
