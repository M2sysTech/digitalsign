namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190137)]
    public class CriaPendencia : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PENDENCIA",
                this.WithId("PENDENCIA_CODE"),
                new Column("BATCH_CODE", DbType.Int32),
                new Column("PROC_CODE", DbType.Int32),
                new Column("PEND_TYPE", DbType.AnsiString, 1, ColumnProperty.NotNull),
                new Column("PEND_TYPEDESC", DbType.AnsiString, 128, ColumnProperty.NotNull),
                new Column("PEND_DESC", DbType.AnsiString, 256),
                new Column("PEND_DATE", DbType.DateTime),
                new Column("PEND_STATUS", DbType.AnsiString, 2),
                new Column("AGENCIA_NUM", DbType.AnsiString, 5),
                new Column("USR_CODECRIADOR", DbType.Int32),
                new Column("USR_CODERESPONSAVEL", DbType.Int32),
                new Column("MDOC_CODE", DbType.Int32),
                new Column("PEND_STARTTIME", DbType.DateTime),
                new Column("PENDENCIA_DATE", DbType.DateTime, ColumnProperty.NotNull),
                new Column("TRANS_CODE", DbType.Int32),
                new Column("DOC_CODE", DbType.Int32),
                new Column("TYPEDOC_ID", DbType.Int32),
                new Column("TRANS_STATUS", DbType.AnsiString, 1),
                new Column("BATCH_AGENCIA", DbType.AnsiString, 4),
                new Column("DOC_BANCO", DbType.AnsiString, 3),
                new Column("DOC_INFO", DbType.AnsiString, 127),
                new Column("DOC_ORIGEM", DbType.Int32),
                new Column("DOC_VALOR", DbType.Decimal),
                new Column("DOC_PEND", DbType.Decimal),
                new Column("TRANS_PEND", DbType.Decimal),
                new Column("PEND_DADOS", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_PENDENCIA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PENDENCIA");
            this.Database.RemoveSequence("SEQ_PENDENCIA");
        }
    }
}
