namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190191)]
    public class CriaSolinv : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "SOLINV",
                this.WithId("SOLINV_CODE"),
                new Column("DOC_CODE", DbType.Int32),
                new Column("SOLINV_DT", DbType.DateTime),
                new Column("SOLINV_USR", DbType.AnsiString, 127),
                new Column("SOLINV_PV", DbType.AnsiString, 4));

            this.Database.CreateSequence("SEQ_SOLINV");
        }

        public override void Down()
        {
            this.Database.RemoveTable("SOLINV");
            this.Database.RemoveSequence("SEQ_SOLINV");
        }
    }
}
