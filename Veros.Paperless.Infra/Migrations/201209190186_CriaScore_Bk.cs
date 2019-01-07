namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190186)]
    public class CriaScoreBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "SCORE_BK",
                this.WithId("SCORE_CODE"),
                new Column("DOC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("TYPEDOC_ID", DbType.Int32),
                new Column("DOC_VALOR", DbType.Decimal),
                new Column("DOC_CMC7", DbType.AnsiString, 1),
                new Column("SCORE_AMOUNT", DbType.Int32),
                new Column("SCORE_CMC7", DbType.Int32),
                new Column("SCORE_SIGNATURE", DbType.Int32),
                new Column("SCORE_METODO", DbType.AnsiString, 1),
                new Column("SCORE_FIELD", DbType.AnsiString, 127),
                new Column("SCORE_MOTOR", DbType.AnsiString, 1),
                new Column("SCORE_NOMINATIVO", DbType.AnsiString, 1),
                new Column("SCORE_DATA", DbType.AnsiString, 1),
                new Column("SCORE_EXTENSO", DbType.AnsiString, 1),
                new Column("SCORE_VERSO", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_SCORE_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("SCORE_BK");
            this.Database.RemoveSequence("SEQ_SCORE_BK");
        }
    }
}
