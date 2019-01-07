namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190192)]
    public class CriaSoljpg : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "SOLJPG",
                this.WithId("SOLJPG_CODE"),
                new Column("DOC_CODE", DbType.Int32),
                new Column("SOLJPG_DT", DbType.DateTime),
                new Column("SOLJPG_USR", DbType.AnsiString, 127),
                new Column("SOLJPG_PV", DbType.AnsiString, 4));

            this.Database.CreateSequence("SEQ_SOLJPG");
        }

        public override void Down()
        {
            this.Database.RemoveTable("SOLJPG");
            this.Database.RemoveSequence("SEQ_SOLJPG");
        }
    }
}
