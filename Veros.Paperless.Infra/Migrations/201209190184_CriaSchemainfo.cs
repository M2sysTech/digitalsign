namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190184)]
    public class CriaSchemainfo : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "SCHEMAINFO",
                this.WithId("SCHEMAINFO_CODE"));

            this.Database.CreateSequence("SEQ_SCHEMAINFO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("SCHEMAINFO");
            this.Database.RemoveSequence("SEQ_SCHEMAINFO");
        }
    }
}
