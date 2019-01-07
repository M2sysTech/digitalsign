namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190083)]
    public class CriaIcr : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ICR",
                this.WithId("ICR_CODE"),
                new Column("ICR_ENABLED", DbType.AnsiString, 1),
                new Column("ICR_SCORE", DbType.Int32),
                new Column("ICR_TBL", DbType.AnsiString, 127),
                new Column("ICR_CMC7", DbType.AnsiString, 1),
                new Column("ICR_SIGNATURE", DbType.AnsiString, 1),
                new Column("ICR_DIGIT", DbType.AnsiString, 1),
                new Column("ICR_JPGAUTOMAT", DbType.AnsiString, 1),
                new Column("ICR_ENGINE1", DbType.AnsiString, 1),
                new Column("ICR_ENGINE2", DbType.AnsiString, 1),
                new Column("ICR_ENGINE3", DbType.AnsiString, 1),
                new Column("ICR_FXVALOR1", DbType.Int32),
                new Column("ICR_FXVALOR2", DbType.Int32),
                new Column("ICR_VALNUM", DbType.AnsiString, 1),
                new Column("ICR_VALEXT", DbType.AnsiString, 1),
                new Column("ICR_DATA", DbType.AnsiString, 1),
                new Column("ICR_CPF", DbType.AnsiString, 1),
                new Column("ICR_NOMINAT", DbType.AnsiString, 1),
                new Column("ICR_ASSINAT", DbType.AnsiString, 1),
                new Column("ICR_FRAUDE", DbType.AnsiString, 1),
                new Column("ICR_VERSO", DbType.AnsiString, 1),
                new Column("ICR_VMAX_NUM", DbType.Decimal),
                new Column("ICR_VMAX_EXT", DbType.Decimal),
                new Column("ICR_VMAX_DATA", DbType.Decimal),
                new Column("ICR_VMAX_CPF", DbType.Decimal),
                new Column("ICR_VMAX_NOM", DbType.Decimal),
                new Column("ICR_VMAX_ASS", DbType.Decimal),
                new Column("ICR_VMAX_FRAUD", DbType.Decimal),
                new Column("ICR_VMAX_VERSO", DbType.Decimal),
                new Column("ICR_MAXBATCH", DbType.Int32),
                new Column("ICR_MAXDOC", DbType.Int32),
                new Column("ICR_DONGLE1", DbType.AnsiString, 127),
                new Column("ICR_DONGLE2", DbType.AnsiString, 127),
                new Column("ICR_CHECKDV", DbType.AnsiString, 1),
                new Column("ICR_DONGLE3", DbType.AnsiString, 127),
                new Column("ICR_DONGLE4", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_ICR");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ICR");
            this.Database.RemoveSequence("SEQ_ICR");
        }
    }
}
