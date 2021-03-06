namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190196)]
    public class CriaSystem : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "SYSTEM",
                this.WithId("SYSTEM_CODE"),
                new Column("SYSTEM_DATE", DbType.DateTime, ColumnProperty.NotNull),
                new Column("SYSTEM_ADMIN", DbType.AnsiString, 127),
                new Column("SYSTEM_EMAIL", DbType.AnsiString, 127),
                new Column("SYSTEM_PWDCHANGE", DbType.Int32),
                new Column("SYSTEM_PWDWRONG", DbType.Int32),
                new Column("SYSTEM_PATHSRVR", DbType.AnsiString, 127),
                new Column("SYSTEM_PATHLOCAL", DbType.AnsiString, 127),
                new Column("SYSTEM_PATHSAIDA", DbType.AnsiString, 127),
                new Column("SYSTEM_BCOREMET", DbType.AnsiString, 9),
                new Column("SYSTEM_FTPIP", DbType.AnsiString, 127),
                new Column("SYSTEM_FTPPORT", DbType.Int32),
                new Column("SYSTEM_FTPUSR", DbType.AnsiString, 127),
                new Column("SYSTEM_FTPPWD", DbType.AnsiString, 127),
                new Column("SYSTEM_FTPTOUT", DbType.Int32),
                new Column("SYSTEM_QUEUEIP", DbType.AnsiString, 127),
                new Column("SYSTEM_QUEUEMODO", DbType.AnsiString, 1),
                new Column("SYSTEM_QUEUEORD", DbType.Int32),
                new Column("SYSTEM_QUEUEAUX", DbType.AnsiString, 127),
                new Column("SYSTEM_QUEUESIZE", DbType.Int32),
                new Column("SYSTEM_QUEUEPORT", DbType.Int32),
                new Column("SYSTEM_PATHICHECK", DbType.AnsiString, 127),
                new Column("SYSTEM_RPTIPSQLP", DbType.AnsiString, 16),
                new Column("SYSTEM_RPTPTSQLP", DbType.Int32),
                new Column("SYSTEM_TCPIPSQLR", DbType.AnsiString, 16),
                new Column("SYSTEM_DELAY1", DbType.Int32),
                new Column("SYSTEM_DELAY2", DbType.Int32),
                new Column("SYSTEM_DELAY3", DbType.Int32),
                new Column("SYSTEM_DELAY4", DbType.Int32),
                new Column("SYSTEM_DELAY5", DbType.Int32),
                new Column("SYSTEM_DELAY6", DbType.Int32),
                new Column("SYSTEM_DEFAGENC", DbType.AnsiString, 127),
                new Column("SYSTEM_DEFCONTA", DbType.AnsiString, 127),
                new Column("SYSTEM_DEFENVEL", DbType.AnsiString, 127),
                new Column("SYSTEM_DELETEIMG", DbType.AnsiString, 1),
                new Column("SYSTEM_CQCAP", DbType.AnsiString, 1),
                new Column("SYSTEM_MASKCELSP", DbType.AnsiString, 127),
                new Column("SYSTEM_MASKCELIF", DbType.AnsiString, 127),
                new Column("SYSTEM_RANGECELI", DbType.AnsiString, 127),
                new Column("SYSTEM_RANGECELF", DbType.AnsiString, 127),
                new Column("SYSTEM_ULTNUMCEL", DbType.AnsiString, 127),
                new Column("SYSTEM_TRATACEL", DbType.AnsiString, 127),
                new Column("SYSTEM_HOSTMODO", DbType.AnsiString, 1),
                new Column("SYSTEM_HOSTMASKF", DbType.AnsiString, 16),
                new Column("SYSTEM_HOSTMASKT", DbType.AnsiString, 16),
                new Column("SYSTEM_HOSTPATH", DbType.AnsiString, 127),
                new Column("SYSTEM_HOSTIP", DbType.AnsiString, 16),
                new Column("SYSTEM_HOSTRPORT", DbType.Int32),
                new Column("SYSTEM_HOSTLPORT", DbType.Int32),
                new Column("SYSTEM_HOSTMAXCO", DbType.Int32),
                new Column("SYSTEM_HOSTDOC", DbType.AnsiString, 1),
                new Column("SYSTEM_REFRESH", DbType.Int32),
                new Column("SYSTEM_RECAPDOC", DbType.AnsiString, 1),
                new Column("SYSTEM_AUTENT", DbType.AnsiString, 1),
                new Column("SYSTEM_CODREDE", DbType.AnsiString, 16),
                new Column("SYSTEM_FORMTDEP", DbType.AnsiString, 1),
                new Column("SYSTEM_VALTDEP", DbType.Decimal),
                new Column("SYSTEM_PORTTDEP", DbType.Int32),
                new Column("SYSTEM_FORMTPAG", DbType.AnsiString, 1),
                new Column("SYSTEM_VALTPAG", DbType.Decimal),
                new Column("SYSTEM_PORTTPAG", DbType.Int32),
                new Column("SYSTEM_FORMTMFOR", DbType.AnsiString, 1),
                new Column("SYSTEM_PORTTMFOR", DbType.Int32),
                new Column("SYSTEM_FORMDEP1", DbType.AnsiString, 1),
                new Column("SYSTEM_FORMPAG1", DbType.AnsiString, 1),
                new Column("SYSTEM_FORMPAG2", DbType.AnsiString, 1),
                new Column("SYSTEM_FORMTORD", DbType.AnsiString, 1),
                new Column("SYSTEM_TEMPOTX", DbType.Int32),
                new Column("SYSTEM_QUANTTX", DbType.Int32),
                new Column("SYSTEM_RMEVALOR1", DbType.Decimal),
                new Column("SYSTEM_RMEVALOR2", DbType.Decimal),
                new Column("SYSTEM_SIMULA", DbType.AnsiString, 1),
                new Column("SYSTEM_SIMULAHOST", DbType.AnsiString, 1),
                new Column("SYSTEM_DIRSIGDB", DbType.AnsiString, 127),
                new Column("SYSTEM_FTPROOT", DbType.AnsiString, 127),
                new Column("SYSTEM_DIRSICCP", DbType.AnsiString, 127),
                new Column("SYSTEM_CAF501", DbType.DateTime),
                new Column("SYSTEM_CAF502", DbType.DateTime),
                new Column("SYSTEM_TREC", DbType.DateTime),
                new Column("SYSTEM_TCBR", DbType.DateTime),
                new Column("SYSTEM_TPAR", DbType.DateTime),
                new Column("SYSTEM_TSUB", DbType.DateTime),
                new Column("SYSTEM_TENT", DbType.DateTime),
                new Column("SYSTEM_QUEUEPOR2", DbType.Int32),
                new Column("SYSTEM_TCPIPATM", DbType.AnsiString, 16),
                new Column("SYSTEM_TCPPTATM", DbType.Int32),
                new Column("SYSTEM_QUEUE_BAT", DbType.Int32),
                new Column("SYSTEM_QUEUE_DOC", DbType.Int32),
                new Column("SYSTEM_LIMDEP", DbType.Decimal),
                new Column("SYSTEM_CONSBLOQ1", DbType.AnsiString, 127),
                new Column("SYSTEM_CONSBLOQ2", DbType.AnsiString, 127),
                new Column("SYSTEM_ATMUPDATE", DbType.DateTime),
                new Column("SYSTEM_TRAVACHK", DbType.AnsiString, 1),
                new Column("SYSTEM_TARE", DbType.DateTime),
                new Column("SYSTEM_TRJE", DbType.DateTime),
                new Column("SYSTEM_TUNI", DbType.DateTime),
                new Column("SYSTEM_SIICO", DbType.DateTime),
                new Column("SYSTEM_TEVP", DbType.DateTime),
                new Column("SYSTEM_TGPS", DbType.DateTime),
                new Column("SYSTEM_TCON", DbType.DateTime),
                new Column("SYSTEM_TRGT", DbType.DateTime),
                new Column("SYSTEM_FILEATM", DbType.AnsiString, 127),
                new Column("SYSTEM_QUEUECHV", DbType.Int32),
                new Column("SYSTEM_TCPIPSQLP", DbType.AnsiString, 16),
                new Column("SYSTEM_TCPPTSQLP", DbType.Int32),
                new Column("SYSTEM_MXRECHOST", DbType.Int32),
                new Column("SYSTEM_MXRECCHK", DbType.Int32),
                new Column("SYSTEM_PORTCHAT", DbType.Int32),
                new Column("SYSTEM_HORAHOST", DbType.DateTime),
                new Column("SYSTEM_FILETXMODE", DbType.AnsiString, 1),
                new Column("SYSTEM_TXRATE", DbType.Int32),
                new Column("SYSTEM_CXONLINE", DbType.AnsiString, 1),
                new Column("SYSTEM_AUTOSUPERV", DbType.AnsiString, 1),
                new Column("SYSTEM_PORTMASTER", DbType.Int32),
                new Column("SYSTEM_TCPPTSQLR", DbType.Int32),
                new Column("SYSTEM_LOGCHK", DbType.AnsiString, 1),
                new Column("SYSTEM_LOGHOST", DbType.AnsiString, 1),
                new Column("SYSTEM_NUMCANAL", DbType.AnsiString, 2),
                new Column("SYSTEM_TIMEBLOQ", DbType.Int32),
                new Column("SYSTEM_TIMEBLOQ2", DbType.Int32),
                new Column("SYSTEM_CONTCAP", DbType.AnsiString, 1),
                new Column("SYSTEM_HWAITMSG", DbType.Int32),
                new Column("SYSTEM_ALCDEB", DbType.Decimal),
                new Column("SYSTEM_ALCCRED", DbType.Decimal),
                new Column("SYSTEM_ALCSALDO", DbType.Decimal),
                new Column("SYSTEM_ALCDOC", DbType.Decimal),
                new Column("SYSTEM_LIMDIF", DbType.Decimal),
                new Column("SYSTEM_VALIDCHQ", DbType.Int32),
                new Column("SYSTEM_LIMITE", DbType.Decimal),
                new Column("SYSTEM_LIMITEX", DbType.Decimal),
                new Column("SYSTEM_LIMLIQDEP", DbType.Decimal),
                new Column("SYSTEM_LIMLIQDPX", DbType.Decimal),
                new Column("SYSTEM_LIMLIQPAG", DbType.Decimal),
                new Column("SYSTEM_HORACORT1", DbType.AnsiString, 5),
                new Column("SYSTEM_HORACORT2", DbType.AnsiString, 5),
                new Column("SYSTEM_APROVAERR", DbType.AnsiString, 1),
                new Column("SYSTEM_APROVAERP", DbType.AnsiString, 1),
                new Column("SYSTEM_CONSCHDEP", DbType.AnsiString, 1),
                new Column("SYSTEM_RESJPG", DbType.Int32),
                new Column("SYSTEM_COBAUTTAR", DbType.AnsiString, 1),
                new Column("SYSTEM_LIMVOUCAS", DbType.Decimal),
                new Column("SYSTEM_PRIBAIXA", DbType.Int32),
                new Column("SYSTEM_PRIMEDIA", DbType.Int32),
                new Column("SYSTEM_PRIALTA", DbType.Int32),
                new Column("SYSTEM_REGULA", DbType.AnsiString, 1),
                new Column("SYSTEM_PRIORIZA", DbType.AnsiString, 1),
                new Column("SYSTEM_SISTEMALEGADOAPP", DbType.AnsiString, 512),
                new Column("SYSTEM_SISTEMALEGADOID", DbType.AnsiString, 128),
                new Column("SYSTEM_SISTEMALEGADOCLASS", DbType.AnsiString, 128),
                new Column("SYSTEM_DIAS_TRIAL", DbType.AnsiString, 16),
                new Column("SYSTEM_PATHRETORNO", DbType.AnsiString, 1000));

            this.Database.CreateSequence("SEQ_SYSTEM");
        }

        public override void Down()
        {
            this.Database.RemoveTable("SYSTEM");
            this.Database.RemoveSequence("SEQ_SYSTEM");
        }
    }
}
