namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190145)]
    public class CriaPgaImages : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PGA_IMAGES",
                this.WithId("PGA_CODE"),
                new Column("IMAGESOURCE", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_PGA_IMAGES");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PGA_IMAGES");
            this.Database.RemoveSequence("SEQ_PGA_IMAGES");
        }
    }
}
