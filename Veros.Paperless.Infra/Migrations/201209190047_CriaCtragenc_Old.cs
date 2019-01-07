namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190047)]
    public class CriaCtragencOld : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CTRAGENC_OLD",
                this.WithId("CTRAGENC_CODE"),
                new Column("AGENCIA_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("CTRAGENC_DTMOV", DbType.DateTime, ColumnProperty.NotNull),
                new Column("CTRAGENC_DTMOVAN", DbType.DateTime),
                new Column("CTRAGENC_DATAI", DbType.DateTime),
                new Column("CTRAGENC_HORAI", DbType.AnsiString, 8),
                new Column("CTRAGENC_USRI", DbType.Int32),
                new Column("CTRAGENC_DATAPF", DbType.DateTime),
                new Column("CTRAGENC_HORAPF", DbType.AnsiString, 8),
                new Column("CTRAGENC_USRPF", DbType.Int32),
                new Column("CTRAGENC_DATAF", DbType.DateTime),
                new Column("CTRAGENC_HORAF", DbType.AnsiString, 8),
                new Column("CTRAGENC_USRF", DbType.Int32),
                new Column("CTRAGENC_OBS", DbType.AnsiString, 4000),
                new Column("CTRAGENC_DURACAO", DbType.Int32),
                new Column("CTRAGENC_NSUABA", DbType.AnsiString, 16),
                new Column("CTRAGENC_STAUX", DbType.AnsiString, 1),
                new Column("CTRAGENC_QTBTCH", DbType.Int32),
                new Column("CTRAGENC_QTDOCS", DbType.Int32),
                new Column("CTRAGENC_FUSO", DbType.Int32),
                new Column("CTRAGENC_STATUS", DbType.AnsiString, 1),
                new Column("CTRAGENC_FCHFORC", DbType.AnsiString, 1),
                new Column("CTRAGENC_DBID", DbType.AnsiString, 3));

            this.Database.CreateSequence("SEQ_CTRAGENC_OLD");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CTRAGENC_OLD");
            this.Database.RemoveSequence("SEQ_CTRAGENC_OLD");
        }
    }
}
