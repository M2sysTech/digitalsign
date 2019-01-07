namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190226)]
    public class CriaUsrlog : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "USRLOG",
                this.WithId("USRLOG_CODE"),
                new Column("AREA_CODE", DbType.Int32),
                new Column("USR_NAME", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("USR_PWD", DbType.AnsiString, 127),
                new Column("USR_OBS", DbType.AnsiString, 4000),
                new Column("USR_MAIL", DbType.AnsiString, 127),
                new Column("USR_ACTIVE", DbType.AnsiString, 1),
                new Column("USR_TYPE", DbType.AnsiString, 1),
                new Column("USR_GROUP", DbType.Int32),
                new Column("USR_LASTLOGIN", DbType.DateTime),
                new Column("USR_LASTCHANGE", DbType.DateTime),
                new Column("USR_DELETED", DbType.AnsiString, 1),
                new Column("USR_PWDWRONG", DbType.Int32),
                new Column("USR_DIGITGROUP", DbType.AnsiString, 4),
                new Column("USR_NETLOGIN", DbType.AnsiString, 127),
                new Column("USR_IP", DbType.AnsiString, 127),
                new Column("QPRIORITY_CODE", DbType.Int32),
                new Column("USR_MATRICULA", DbType.AnsiString, 16),
                new Column("USR_CHAT", DbType.AnsiString, 1),
                new Column("USR_QUEUETEMP", DbType.Int32),
                new Column("USR_SENDMAIL", DbType.AnsiString, 1),
                new Column("USR_DTEXP", DbType.DateTime),
                new Column("USR_NIVEL", DbType.Int32),
                new Column("USR_STATUSPWD", DbType.AnsiString, 1),
                new Column("USR_TOTDOCDIG", DbType.Int32),
                new Column("USR_TEMPDIG", DbType.Int32),
                new Column("USR_TEMDTNASC", DbType.DateTime),
                new Column("USR_TELEF", DbType.AnsiString, 127),
                new Column("USR_LOGGED", DbType.AnsiString, 1),
                new Column("USR_ALCCREDITO", DbType.Decimal),
                new Column("USR_ALCDEBITO", DbType.Decimal),
                new Column("USR_ALCDOCTED", DbType.Decimal),
                new Column("USR_ALCINDISP", DbType.Decimal),
                new Column("USR_ALCESTORNO", DbType.AnsiString, 1),
                new Column("USR_ALCPREAUTO", DbType.AnsiString, 1),
                new Column("USR_ALCPARTCOM", DbType.AnsiString, 1),
                new Column("USR_RESPGROUP", DbType.Int32),
                new Column("USR_TOTDOCFORM", DbType.Int32),
                new Column("USR_TEMPFORM", DbType.Int32),
                new Column("USR_LOCALTRAB", DbType.Int32),
                new Column("USR_AREASIGLA", DbType.AnsiString, 2),
                new Column("USR_SUBSTTEMP", DbType.AnsiString, 1),
                new Column("USR_USRSUBST", DbType.Int32),
                new Column("USR_MOTIVOSUBST", DbType.AnsiString, 4000),
                new Column("PREST_CODE", DbType.Int32),
                new Column("USR_UPDATEDATE", DbType.DateTime),
                new Column("USR_UPDATENAME", DbType.AnsiString, 127),
                new Column("USR_SUBSTMATR", DbType.AnsiString, 127),
                new Column("USR_SUBSTDATE", DbType.DateTime),
                new Column("USR_SUBSTTIPO", DbType.AnsiString, 1),
                new Column("USR_SUBSTCODE", DbType.Int32),
                new Column("USR_ALLOWPRINT", DbType.AnsiString, 1),
                new Column("GRUPOMON_CODE", DbType.Int32),
                new Column("USR_REAUTENTDOC", DbType.AnsiString, 1),
                new Column("PREFERENC_CODE", DbType.Int32),
                new Column("USR_SHOWLISTCA", DbType.AnsiString, 1),
                new Column("USR_NIVELAUTORIZ", DbType.Int32),
                new Column("USR_FINGER", DbType.AnsiString, 1),
                new Column("USR_MATSUBSTR", DbType.AnsiString, 127),
                new Column("USR_PERFILSIGLA", DbType.AnsiString, 127),
                new Column("USR_SUBSTSUPERIOR", DbType.AnsiString, 127),
                new Column("USR_AREAPERFIL", DbType.AnsiString, 127),
                new Column("USR_DBID", DbType.AnsiString, 127),
                new Column("CLIENTE_CODE", DbType.Int32),
                new Column("EQUIPE_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_USRLOG");
        }

        public override void Down()
        {
            this.Database.RemoveTable("USRLOG");
            this.Database.RemoveSequence("SEQ_USRLOG");
        }
    }
}
