namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190153)]
    public class CriaPrest : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PREST",
                this.WithId("PREST_CODE"),
                new Column("PREST_NAME", DbType.AnsiString, 254, ColumnProperty.NotNull),
                new Column("PREST_ALIAS", DbType.AnsiString, 16, ColumnProperty.NotNull),
                new Column("PREST_ADDRESS", DbType.AnsiString, 127),
                new Column("PREST_CITY", DbType.AnsiString, 127),
                new Column("PREST_ZIP", DbType.AnsiString, 16),
                new Column("PREST_PHONE", DbType.AnsiString, 127),
                new Column("PREST_FAX", DbType.AnsiString, 127),
                new Column("PREST_EMAIL", DbType.AnsiString, 127),
                new Column("PREST_CNPJ", DbType.AnsiString, 254),
                new Column("PREST_RESP", DbType.AnsiString, 127),
                new Column("PREST_CPF", DbType.AnsiString, 16),
                new Column("PREST_CARGO", DbType.AnsiString, 127),
                new Column("PREST_RG", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_PREST");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PREST");
            this.Database.RemoveSequence("SEQ_PREST");
        }
    }
}
