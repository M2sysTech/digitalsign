namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190155)]
    public class CriaProbot : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PROBOT",
                this.WithId("PROBOT_CODE"),
                new Column("PROBOT_DESC", DbType.AnsiString, 127),
                new Column("PROBOT_IMGTYPE", DbType.AnsiString, 1),
                new Column("BATCH_TYPE", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_PROBOT");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PROBOT");
            this.Database.RemoveSequence("SEQ_PROBOT");
        }
    }
}
