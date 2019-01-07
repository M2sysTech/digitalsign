namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190039)]
    public class CriaConces : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CONCES",
                this.WithId("CONCES_CODE"),
                new Column("CONCES_PREFIXO", DbType.AnsiString, 16, ColumnProperty.NotNull),
                new Column("CONCES_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("CONCES_OBS", DbType.AnsiString, 4000),
                new Column("CONCES_SEGMENTO", DbType.AnsiString, 2),
                new Column("CONCES_UF", DbType.AnsiString, 2),
                new Column("CONCES_ST", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_CONCES");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CONCES");
            this.Database.RemoveSequence("SEQ_CONCES");
        }
    }
}
