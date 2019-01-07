namespace Veros.Data.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201306101526)]
    public class CriaSeedInfo : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "seedinfo",
                this.WithId(),
                new Column("name", DbType.String, 64),
                new Column("version", DbType.String, 64));

            this.Database.CreateSequence("seq_seedinfo");
        }

        public override void Down()
        {
            this.Database.RemoveTable("seedinfo");
            this.Database.RemoveSequence("seq_seedinfo");
        }
    }
}
