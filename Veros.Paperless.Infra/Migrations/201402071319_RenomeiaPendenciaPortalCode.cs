namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201402071319)]
    public class RenomeiaPendenciaPortalCode : Migration
    {
        public override void Up()
        {
            this.Database.RenameColumn("pendenciaportal", "pendenciarecepcao_code", "pendenciaportal_code");
        }

        public override void Down()
        {
        }
    }
}
