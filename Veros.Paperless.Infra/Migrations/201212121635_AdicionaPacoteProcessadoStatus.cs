namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201212121635)]
    public class AdicionaPacoteProcessadoStatus : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PACOTEPROCESSADO", new Column("PACOTEPROCESSADO_STATUS", DbType.Int32 ));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("PACOTEPROCESSADO", "PACOTEPROCESSADO_STATUS");
        }
    }
}