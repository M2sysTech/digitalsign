namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190214)]
    public class CriaTuni : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TUNI",
                this.WithId("TUNI_CODE"),
                new Column("TUNI_UNIDADE", DbType.AnsiString, 5, ColumnProperty.NotNull),
                new Column("TUNI_TIPOUNI", DbType.AnsiString, 1),
                new Column("TUNI_DESCRICAO", DbType.AnsiString, 127),
                new Column("TUNI_FORMATO", DbType.AnsiString, 6),
                new Column("TUNI_UF", DbType.AnsiString, 2),
                new Column("TUNI_ULTSIT", DbType.AnsiString, 2),
                new Column("TUNI_ST", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_TUNI");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TUNI");
            this.Database.RemoveSequence("SEQ_TUNI");
        }
    }
}
