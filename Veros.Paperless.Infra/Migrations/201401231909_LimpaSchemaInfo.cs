namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201401231909)]
    public class LimpaSchemaInfo : Migration
    {
        public override void Up()
        {
            this.Database.Delete("schemainfo", "version", "20120529154335");
        }

        public override void Down()
        {
        }
    }
}
