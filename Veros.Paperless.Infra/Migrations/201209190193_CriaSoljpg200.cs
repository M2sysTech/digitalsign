namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190193)]
    public class CriaSoljpg200 : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "SOLJPG200",
                this.WithId("SOLJPG200_CODE"),
                new Column("DOC_CODE", DbType.Int32),
                new Column("SOLJPG200_DT", DbType.DateTime),
                new Column("SOLJPG200_USR", DbType.AnsiString, 127),
                new Column("SOLJPG200_PV", DbType.AnsiString, 4));

            this.Database.CreateSequence("SEQ_SOLJPG200");
        }

        public override void Down()
        {
            this.Database.RemoveTable("SOLJPG200");
            this.Database.RemoveSequence("SEQ_SOLJPG200");
        }
    }
}
