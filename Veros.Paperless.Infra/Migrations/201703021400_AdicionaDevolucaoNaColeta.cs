namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201703021400)]
    public class AdicionaDevolucaoNaColeta : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("COLETA", new Column("COLETA_DTDEVOLUCAO", DbType.Date));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("COLETA", "COLETA_DTDEVOLUCAO");
        }
    }
}
