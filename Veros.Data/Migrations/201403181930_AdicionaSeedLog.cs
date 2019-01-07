namespace Veros.Data.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201403181930)]
    public class AdicionaSeedLog : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "seedlog",
                this.WithId(),
                new Column("name", DbType.String, 64),
                new Column("version", DbType.String, 64),
                new Column("appversion", DbType.String, 64),
                new Column("executed_at", DbType.DateTime),
                new Column("machine", DbType.String, StringLength.S128));

            this.Database.CreateSequence("seq_seedlog");
        }

        public override void Down()
        {
            this.Database.RemoveTable("seedlog");
            this.Database.RemoveSequence("seq_seedlog");
        }
    }
}
