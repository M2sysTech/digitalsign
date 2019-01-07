namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201212180936)]
    public class AdicionaValorUtilizadoParaValorFinal : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOCDADOS", new Column("MDOCDADOS_ELEITO", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("MDOCDADOS", "MDOCDADOS_ELEITO");
        }
    }
}