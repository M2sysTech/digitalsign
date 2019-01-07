namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190023)]
    public class CriaBatchLix : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "BATCH_LIX",
                this.WithId("BATCH_CODE"),
                new Column("PACK_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("BATCH_STATUS", DbType.AnsiString, 2),
                new Column("BATCH_BATIDO", DbType.AnsiString, 1),
                new Column("BATCH_HASH", DbType.AnsiString, 127),
                new Column("BATCH_NUMDOCS", DbType.Int32),
                new Column("BATCH_AGENCIA", DbType.AnsiString, 4),
                new Column("BATCH_QUEUEPRIOR", DbType.AnsiString, 1),
                new Column("REMESSA_CODE", DbType.Int32),
                new Column("BATCH_TXTENT", DbType.Int32),
                new Column("BATCH_BINTENT", DbType.Int32),
                new Column("BATCH_RESUMO", DbType.AnsiString, 127),
                new Column("BATCH_GROUP", DbType.AnsiString, 1),
                new Column("BATCH_TYPE", DbType.AnsiString, 1),
                new Column("BATCH_XTYPE", DbType.AnsiString, 1),
                new Column("BATCH_TDC", DbType.AnsiString, 1),
                new Column("BATCH_USRRESP", DbType.Int32),
                new Column("BATCH_ICR_CMC7", DbType.Int32),
                new Column("BATCH_ICR_VALOR", DbType.Int32),
                new Column("BATCH_ICR_AMBOS", DbType.Int32),
                new Column("BATCH_PERFREADER", DbType.Decimal),
                new Column("BATCH_PERFICR", DbType.Decimal),
                new Column("BATCH_PERFSCAN", DbType.Int32),
                new Column("BATCH_STAUX", DbType.AnsiString, 1),
                new Column("BATCH_XAGENCIA", DbType.AnsiString, 4),
                new Column("BATCH_TCAP", DbType.DateTime),
                new Column("BATCH_TTX", DbType.DateTime),
                new Column("BATCH_TICR", DbType.DateTime),
                new Column("BATCH_TRECAP", DbType.DateTime),
                new Column("BATCH_TDIGIT", DbType.DateTime),
                new Column("BATCH_TVALID", DbType.DateTime),
                new Column("BATCH_TVERSO", DbType.DateTime),
                new Column("BATCH_TCRUZADO", DbType.DateTime),
                new Column("BATCH_TMONTA", DbType.DateTime),
                new Column("BATCH_TORDENADO", DbType.DateTime),
                new Column("BATCH_TFIM", DbType.DateTime),
                new Column("BATCH_IMPEXTRATO", DbType.Int32),
                new Column("BATCH_PRIORITY", DbType.Int32),
                new Column("BATCH_MOTIVO", DbType.AnsiString, 127),
                new Column("BATCH_NUMPAG", DbType.Int32),
                new Column("BATCH_NUMPROCESSO", DbType.AnsiString, 127),
                new Column("BATCH_CAIXA", DbType.AnsiString, 120),
                new Column("BATCH_STARTTIME", DbType.DateTime),
                new Column("BATCH_DT", DbType.DateTime),
                new Column("BATCH_VALOR", DbType.Decimal),
                new Column("BATCH_TBIN", DbType.DateTime),
                new Column("BATCH_TPRZ", DbType.DateTime),
                new Column("BATCH_TEXPORT", DbType.DateTime),
                new Column("BATCH_TMONT", DbType.DateTime),
                new Column("BATCH_TIDENT", DbType.DateTime),
                new Column("BATCH_TBATCH_TMONT", DbType.DateTime),
                new Column("BATCH_TCONSULT", DbType.DateTime),
                new Column("BATCH_TFORMAL", DbType.DateTime),
                new Column("BATCH_TAPROV", DbType.DateTime),
                new Column("BATCH_TAJUSTORIG", DbType.DateTime),
                new Column("BATCH_TENVIO", DbType.DateTime),
                new Column("BATCH_TRETORNO", DbType.DateTime),
                new Column("BATCH_TAJUSTE", DbType.DateTime));

            this.Database.CreateSequence("SEQ_BATCH_LIX");
        }

        public override void Down()
        {
            this.Database.RemoveTable("BATCH_LIX");
            this.Database.RemoveSequence("SEQ_BATCH_LIX");
        }
    }
}
