namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201712201637)]
    public class AdicionaQualidadeCefNaPacoteProcessado : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PACOTEPROCESSADO", new Column("PACOTEPROCESSADO_QUALICEF", DbType.AnsiString, 1));

            this.Database.ExecuteNonQuery("UPDATE PACOTEPROCESSADO SET PACOTEPROCESSADO_QUALICEF = PACOTEPROCESSADO_ATIVADO");
        }

        public override void Down()
        {
        }
    }
}
