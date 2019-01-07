namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190188)]
    public class CriaSegmento : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "SEGMENTO",
                this.WithId("SEGMENTO_CODE"),
                new Column("SEGMENTO_ID", DbType.AnsiString, 5),
                new Column("SEGMENTO_DESC", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_SEGMENTO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("SEGMENTO");
            this.Database.RemoveSequence("SEQ_SEGMENTO");
        }
    }
}
