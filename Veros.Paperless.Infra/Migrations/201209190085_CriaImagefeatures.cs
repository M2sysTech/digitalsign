namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190085)]
    public class CriaImagefeatures : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "IMAGEFEATURES",
                this.WithId("IMAGEFEATURES_CODE"),
                new Column("DOC_CODE", DbType.Int32),
                new Column("IMAGEFEATURES_HRESOLUTION", DbType.Int32),
                new Column("IMAGEFEATURES_VRESOLUTION", DbType.Int32),
                new Column("IMAGEFEATURES_WIDTH", DbType.Int32),
                new Column("IMAGEFEATURES_HEIGHT", DbType.Int32));

            this.Database.CreateSequence("SEQ_IMAGEFEATURES");
        }

        public override void Down()
        {
            this.Database.RemoveTable("IMAGEFEATURES");
            this.Database.RemoveSequence("SEQ_IMAGEFEATURES");
        }
    }
}
