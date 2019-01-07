namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201212041051)]
    public class AdicionaPacoteProcessadoeEnviadoEm : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PACOTEPROCESSADO", new Column("PACOTEPROCESSADO_ENVIADOEM", DbType.DateTime));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("PACOTEPROCESSADO", "PACOTEPROCESSADO_ENVIADOEM");
        }
    }
}