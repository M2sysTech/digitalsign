namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201410081408)]
    public class AdicionaValorRecuperadoNaMDocDados : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOCDADOS", "MDOCDADOS_VALORRECUPERADO", DbType.Int32);
            this.Database.AddColumn("MDOCDADOS_BK", "MDOCDADOS_VALORRECUPERADO", DbType.Int32);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("MDOCDADOS", "MDOCDADOS_VALORRECUPERADO");
            this.Database.RemoveColumn("MDOCDADOS_BK", "MDOCDADOS_VALORRECUPERADO");
        }
    }
}
