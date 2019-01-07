namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190151)]
    public class CriaPmonit : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PMONIT",
                this.WithId("PMONIT_CODE"),
                new Column("PMONIT_VTRANSMIS", DbType.AnsiString, 1),
                new Column("PMONIT_VREADER", DbType.AnsiString, 1),
                new Column("PMONIT_VICR", DbType.AnsiString, 1),
                new Column("PMONIT_VDIGITACA", DbType.AnsiString, 1),
                new Column("PMONIT_VMONTAGEM", DbType.AnsiString, 1),
                new Column("PMONIT_VDIFERENC", DbType.AnsiString, 1),
                new Column("PMONIT_VFORMDOC", DbType.AnsiString, 1),
                new Column("PMONIT_VFORMNEG", DbType.AnsiString, 1),
                new Column("PMONIT_VLIBCENT", DbType.AnsiString, 1),
                new Column("PMONIT_VHOST", DbType.AnsiString, 1),
                new Column("PMONIT_VRECCENT", DbType.AnsiString, 1),
                new Column("PMONIT_TRANSMISS", DbType.Int32),
                new Column("PMONIT_READER", DbType.Int32),
                new Column("PMONIT_ICR", DbType.Int32),
                new Column("PMONIT_DIGITACAO", DbType.Int32),
                new Column("PMONIT_MONTAGEM", DbType.Int32),
                new Column("PMONIT_DIFERENCA", DbType.Int32),
                new Column("PMONIT_FORMDOC", DbType.Int32),
                new Column("PMONIT_FORMNEG", DbType.Int32),
                new Column("PMONIT_LIBCENT", DbType.Int32),
                new Column("PMONIT_HOST", DbType.Int32),
                new Column("PMONIT_RECCENT", DbType.Int32),
                new Column("PMONIT_TOTAL", DbType.Int32),
                new Column("PMONIT_TMAXLOTE", DbType.Int32),
                new Column("PMONIT_TMEDDIG", DbType.Int32),
                new Column("PMONIT_TMAXDIG", DbType.Int32),
                new Column("PMONIT_ALHOST", DbType.AnsiString, 1),
                new Column("PMONIT_ALCHKSRV", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_PMONIT");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PMONIT");
            this.Database.RemoveSequence("SEQ_PMONIT");
        }
    }
}
