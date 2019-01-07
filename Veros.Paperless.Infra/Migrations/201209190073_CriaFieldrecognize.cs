namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190073)]
    public class CriaFieldrecognize : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "FIELDRECOGNIZE",
                this.WithId("FIELDRECOGNIZE_CODE"),
                new Column("MDOC_CODE", DbType.Int32),
                new Column("TYPEDOC_ID", DbType.Int32),
                new Column("FIELDRECOGNIZE_STATUS", DbType.Int32),
                new Column("TDCAMPOS_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_FIELDRECOGNIZE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("FIELDRECOGNIZE");
            this.Database.RemoveSequence("SEQ_FIELDRECOGNIZE");
        }
    }
}
