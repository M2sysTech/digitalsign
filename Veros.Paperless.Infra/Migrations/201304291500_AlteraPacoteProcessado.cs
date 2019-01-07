namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201304291500)]
    public class AlterarPacoteProcessado : Migration
    {
        public override void Up()
        {
            ////this.Database.ChangeColumn("PACOTEPROCESSADO", new Column("PACOTEPROCESSADO_RECEBIDOEM", DbType.Date));
            ////this.Database.ChangeColumn("PACOTEPROCESSADO", new Column("PACOTEPROCESSADO_IMPORTADOEM", DbType.Date));
            ////this.Database.ChangeColumn("PACOTEPROCESSADO", new Column("PACOTEPROCESSADO_ENVIADOEM", DbType.Date));
            this.Database.ExecuteNonQuery("ALTER TABLE PACOTEPROCESSADO MODIFY PACOTEPROCESSADO_RECEBIDOEM DATE");
            this.Database.ExecuteNonQuery("ALTER TABLE PACOTEPROCESSADO MODIFY PACOTEPROCESSADO_IMPORTADOEM DATE");
            this.Database.ExecuteNonQuery("ALTER TABLE PACOTEPROCESSADO MODIFY PACOTEPROCESSADO_ENVIADOEM DATE");
        }

        public override void Down()
        {
        }
    }
}