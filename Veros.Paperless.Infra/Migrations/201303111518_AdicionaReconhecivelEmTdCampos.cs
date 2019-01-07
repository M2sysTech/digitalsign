namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201303111518)]
    public class AdicionaReconhecivelEmTdCampos : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("TDCAMPOS", new Column("TDCAMPOS_RECONHECIVEL", DbType.Boolean));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("TDCAMPOS", "TDCAMPOS_RECONHECIVEL");
        }
    }
}