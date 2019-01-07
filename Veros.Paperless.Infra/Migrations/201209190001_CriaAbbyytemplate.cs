namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190001)]
    public class CriaAbbyytemplate : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ABBYYTEMPLATE",
                this.WithId("ABBYYTEMPLATE_CODE"),
                new Column("ABBYYTEMPLATE_PATH", DbType.AnsiString, 255),
                new Column("ABBYYTEMPLATE_TYPE", DbType.Int32));

            this.Database.CreateSequence("SEQ_ABBYYTEMPLATE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ABBYYTEMPLATE");
            this.Database.RemoveSequence("SEQ_ABBYYTEMPLATE");
        }
    }
}
