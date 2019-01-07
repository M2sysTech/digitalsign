namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190156)]
    public class CriaProc : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PROC",
                this.WithId("PROC_CODE"),
                new Column("BATCH_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("TYPEPROC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("PROC_STATUS", DbType.AnsiString, 2, ColumnProperty.NotNull),
                new Column("PROC_AGENCIA", DbType.AnsiString, 6),
                new Column("PROC_STARTTIME", DbType.DateTime),
                new Column("USR_RESP", DbType.Int32),
                new Column("PROC_ACAO", DbType.Int32),
                new Column("PROC_TCANCELADO", DbType.DateTime),
                new Column("PROC_TFINALIZADO", DbType.DateTime),
                new Column("PROC_MARCA", DbType.AnsiString, 10),
                new Column("REMESSA_CODE", DbType.Int32),
                new Column("PROC_OBS", DbType.AnsiString, 4000),
                new Column("PROC_CONTA", DbType.AnsiString, 30));

            this.Database.CreateSequence("SEQ_PROC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PROC");
            this.Database.RemoveSequence("SEQ_PROC");
        }
    }
}
