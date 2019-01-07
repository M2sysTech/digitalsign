namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190049)]
    public class CriaDarf : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "DARF",
                this.WithId("DARF_CODE"),
                new Column("DARF_ID", DbType.AnsiString, 16, ColumnProperty.NotNull),
                new Column("DARF_DESC", DbType.AnsiString, 127),
                new Column("DARF_INIAPU", DbType.DateTime),
                new Column("DARF_VLRMIN", DbType.Decimal),
                new Column("DARF_REFERENCIA", DbType.AnsiString, 1),
                new Column("DARF_CNPJ", DbType.AnsiString, 1),
                new Column("DARF_CPF", DbType.AnsiString, 1),
                new Column("DARF_CPF191", DbType.AnsiString, 1),
                new Column("DARF_TIPO", DbType.Int32),
                new Column("DARF_MATRIZ", DbType.AnsiString, 1),
                new Column("DARF_ST", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_DARF");
        }

        public override void Down()
        {
            this.Database.RemoveTable("DARF");
            this.Database.RemoveSequence("SEQ_DARF");
        }
    }
}
