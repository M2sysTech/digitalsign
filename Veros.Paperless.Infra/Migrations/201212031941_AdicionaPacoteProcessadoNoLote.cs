namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201212031941)]
    public class AdicionaPacoteProcessadoNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("PACOTEPROCESSADO_CODE", DbType.Int32 ));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("BATCH", "PACOTEPROCESSADO_CODE");
        }
    }
}