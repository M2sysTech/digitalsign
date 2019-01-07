namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190076)]
    public class CriaFormdoccaneta : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "FORMDOCCANETA",
                this.WithId("FORMDOCCANETA_CODE"),
                new Column("TYPEDOC_ID", DbType.Int32, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_FORMDOCCANETA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("FORMDOCCANETA");
            this.Database.RemoveSequence("SEQ_FORMDOCCANETA");
        }
    }
}
