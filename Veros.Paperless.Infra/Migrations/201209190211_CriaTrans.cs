namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190211)]
    public class CriaTrans : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TRANS",
                this.WithId("TRANS_CODE"),
                new Column("BATCH_CODE", DbType.Int32),
                new Column("TRANS_TYPE", DbType.AnsiString, 1),
                new Column("TYPEDOC_ID", DbType.Int32),
                new Column("TRANS_STATUS", DbType.AnsiString, 1),
                new Column("TRANS_STAUX", DbType.AnsiString, 1),
                new Column("TRANS_FORMALIST", DbType.AnsiString, 1),
                new Column("TRANS_BATIDO", DbType.AnsiString, 1),
                new Column("TRANS_MOTIVO", DbType.AnsiString, 254),
                new Column("ENV_CODE", DbType.Int32),
                new Column("TRANS_AMBIENTE", DbType.AnsiString, 1),
                new Column("TRANS_STATUSANT", DbType.AnsiString, 1),
                new Column("TRANS_USRRESP", DbType.Int32),
                new Column("TRANS_USRESTORNO", DbType.Int32),
                new Column("TRANS_TIPOESTORN", DbType.AnsiString, 1),
                new Column("TRANS_PEND", DbType.Decimal),
                new Column("TRANS_IDBDU", DbType.AnsiString, 7),
                new Column("TRANS_IDAGCONTA", DbType.AnsiString, 12),
                new Column("TRANS_CODEORIG", DbType.AnsiString, 10),
                new Column("TRANS_ALCADA", DbType.AnsiString, 2),
                new Column("TRANS_ALCADAVLD", DbType.Decimal),
                new Column("TRANS_ALCADAVLC", DbType.Decimal),
                new Column("TRANS_ALCADAVLI", DbType.Decimal),
                new Column("TRANS_SUPERVISOR", DbType.AnsiString, 127),
                new Column("TRANS_MATSOLEST", DbType.AnsiString, 10),
                new Column("TRANS_CODEMULT", DbType.Int32),
                new Column("TRANS_RELAT", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_TRANS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TRANS");
            this.Database.RemoveSequence("SEQ_TRANS");
        }
    }
}
