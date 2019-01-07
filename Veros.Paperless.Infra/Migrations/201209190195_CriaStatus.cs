namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190195)]
    public class CriaStatus : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "STATUS",
                this.WithId("STATUS_CODE"),
                new Column("STATUS_TAG", DbType.AnsiString, 16),
                new Column("STATUS_ID", DbType.AnsiString, 16),
                new Column("STATUS_DATETIME", DbType.DateTime),
                new Column("STATUS_VERSION", DbType.AnsiString, 25));

            this.Database.CreateSequence("SEQ_STATUS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("STATUS");
            this.Database.RemoveSequence("SEQ_STATUS");
        }
    }
}
