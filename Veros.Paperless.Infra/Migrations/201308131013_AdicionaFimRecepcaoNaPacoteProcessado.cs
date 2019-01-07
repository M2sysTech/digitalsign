namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201308131013)]
    public class AdicionaFimRecepcaoNaPacoteProcessado : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PACOTEPROCESSADO", new Column("PACOTEPROCESSADO_FIMRECEPCAO", DbType.Date));

            var sql = @"ALTER TABLE PACOTEPROCESSADO_BK ADD (PACOTEPROCESSADO_FIMRECEPCAO DATE)";

            this.Database.ExecuteNonQuery(sql);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("PACOTEPROCESSADO", "PACOTEPROCESSADO_FIMRECEPCAO");

            var sql = @"ALTER TABLE PACOTEPROCESSADO DROP COLUMN PACOTEPROCESSADO_FIMRECEPCAO";

            this.Database.ExecuteNonQuery(sql);
        }
    }
}
