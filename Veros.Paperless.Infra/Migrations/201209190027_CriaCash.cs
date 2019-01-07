namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190027)]
    public class CriaCash : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CASH",
                this.WithId("CASH_CODE"),
                new Column("DOC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("TRANS_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("BATCH_CODE", DbType.Int32),
                new Column("BATCH_GROUP", DbType.AnsiString, 1),
                new Column("CASH_AGENCIA", DbType.AnsiString, 4),
                new Column("CASH_VALORDECL", DbType.Decimal),
                new Column("CASH_VALORCONF", DbType.Decimal),
                new Column("CASH_TIPO", DbType.AnsiString, 1),
                new Column("CASH_STATUS", DbType.AnsiString, 1),
                new Column("CASH_MATRUSR", DbType.AnsiString, 8),
                new Column("TRANS_CODEAUX", DbType.Int32));

            this.Database.CreateSequence("SEQ_CASH");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CASH");
            this.Database.RemoveSequence("SEQ_CASH");
        }
    }
}
