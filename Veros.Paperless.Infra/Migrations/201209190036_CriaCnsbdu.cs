namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190036)]
    public class CriaCnsbdu : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CNSBDU",
                this.WithId("CNSBDU_CODE"),
                new Column("DOC_CODE", DbType.Int32),
                new Column("DOC_AGNAME", DbType.AnsiString, 127),
                new Column("DOC_BDUFILHO", DbType.AnsiString, 1),
                new Column("DOC_OSF", DbType.AnsiString, 10));

            this.Database.CreateSequence("SEQ_CNSBDU");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CNSBDU");
            this.Database.RemoveSequence("SEQ_CNSBDU");
        }
    }
}
