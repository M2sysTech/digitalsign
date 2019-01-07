namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201805241334)]
    public class AdicionaStatusNaPendenciaColeta : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PENDENCIACOLETA", new Column("PENDCOL_STATUS", DbType.AnsiString, 3));
        }

        public override void Down()
        {
        }
    }
}
