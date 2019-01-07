namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190138)]
    public class CriaPendenciaBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PENDENCIA_BK",
                this.WithId("PENDENCIA_CODE"),
                new Column("BATCH_CODE", DbType.Int32),
                new Column("PROC_CODE", DbType.Int32),
                new Column("PEND_TYPE", DbType.AnsiString, 1),
                new Column("PEND_TYPEDESC", DbType.AnsiString, 128),
                new Column("PEND_DESC", DbType.AnsiString, 256),
                new Column("PEND_DATE", DbType.DateTime),
                new Column("PEND_STATUS", DbType.AnsiString, 2),
                new Column("AGENCIA_NUM", DbType.AnsiString, 5),
                new Column("USR_CODECRIADOR", DbType.Int32),
                new Column("USR_CODERESPONSAVEL", DbType.Int32),
                new Column("MDOC_CODE", DbType.Int32),
                new Column("PEND_STARTTIME", DbType.DateTime),
                new Column("PEND_DADOS", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_PENDENCIA_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PENDENCIA_BK");
            this.Database.RemoveSequence("SEQ_PENDENCIA_BK");
        }
    }
}
