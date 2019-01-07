namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190158)]
    public class CriaProcBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PROC_BK",
                this.WithId("PROC_CODE"),
                new Column("BATCH_CODE", DbType.Int32),
                new Column("TYPEPROC_CODE", DbType.Int32),
                new Column("PROC_STATUS", DbType.AnsiString, 2),
                new Column("PROC_AGENCIA", DbType.AnsiString, 6),
                new Column("PROC_STARTTIME", DbType.DateTime),
                new Column("USR_RESP", DbType.Int32),
                new Column("PROC_ACAO", DbType.Int32),
                new Column("PROC_TCANCELADO", DbType.DateTime),
                new Column("PROC_TFINALIZADO", DbType.DateTime),
                new Column("PROC_MARCA", DbType.AnsiString, 10),
                new Column("PROC_OBS", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_PROC_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PROC_BK");
            this.Database.RemoveSequence("SEQ_PROC_BK");
        }
    }
}
