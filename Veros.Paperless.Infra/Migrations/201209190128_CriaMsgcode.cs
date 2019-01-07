namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190128)]
    public class CriaMsgcode : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "MSGCODE",
                this.WithId("MSGCODE_CODE"),
                new Column("MSGCODE_CODEORIG", DbType.AnsiString, 10, ColumnProperty.NotNull),
                new Column("MSGCODE_DESC", DbType.AnsiString, 127),
                new Column("MSGCODE_POSERRDIGIT", DbType.AnsiString, 1),
                new Column("MSGCODE_NOVATENT", DbType.AnsiString, 1),
                new Column("MSGCODE_AUTORIZA", DbType.AnsiString, 1),
                new Column("MSGCODE_CANCELA", DbType.AnsiString, 1),
                new Column("MSGCODE_RESOLVEPEND", DbType.AnsiString, 1),
                new Column("MSGCODE_ENVIAMSG200", DbType.AnsiString, 1),
                new Column("MSGCODE_ISRETALCADA", DbType.AnsiString, 1),
                new Column("MSGCODE_ISTIMEOUT", DbType.AnsiString, 1),
                new Column("MSGCODE_INSUFIC", DbType.AnsiString, 1),
                new Column("MSGCODE_INDISP", DbType.AnsiString, 1),
                new Column("MSGCODE_TIPO", DbType.AnsiString, 1),
                new Column("MSGCODE_ALCADA", DbType.AnsiString, 1),
                new Column("MSGCODE_TIPOALCADA", DbType.AnsiString, 2),
                new Column("MSGCODE_ALCA1", DbType.AnsiString, 1),
                new Column("MSGCODE_ALCA2", DbType.AnsiString, 1),
                new Column("MSGCODE_ALCA3", DbType.AnsiString, 1),
                new Column("MSGCODE_ALCA4", DbType.AnsiString, 1),
                new Column("MSGCODE_ALCADAVLR", DbType.AnsiString, 1),
                new Column("MSGCODE_XTIPO", DbType.AnsiString, 1),
                new Column("MSGCODE_XALCADA", DbType.AnsiString, 1),
                new Column("MSGCODE_XTIPOALCADA", DbType.AnsiString, 2),
                new Column("MSGCODE_XALCA1", DbType.AnsiString, 1),
                new Column("MSGCODE_XALCA2", DbType.AnsiString, 1),
                new Column("MSGCODE_XALCA3", DbType.AnsiString, 1),
                new Column("MSGCODE_XALCA4", DbType.AnsiString, 1),
                new Column("MSGCODE_XALCADAVLR", DbType.AnsiString, 1),
                new Column("MSGCODE_X2TIPO", DbType.AnsiString, 1),
                new Column("MSGCODE_X2ALCADA", DbType.AnsiString, 1),
                new Column("MSGCODE_X2TIPOALCADA", DbType.AnsiString, 2),
                new Column("MSGCODE_X2ALCA1", DbType.AnsiString, 1),
                new Column("MSGCODE_X2ALCA2", DbType.AnsiString, 1),
                new Column("MSGCODE_X2ALCA3", DbType.AnsiString, 1),
                new Column("MSGCODE_X2ALCA4", DbType.AnsiString, 1),
                new Column("MSGCODE_X2ALCADAVLR", DbType.AnsiString, 1),
                new Column("MSGCODE_X3TIPO", DbType.AnsiString, 1),
                new Column("MSGCODE_X3ALCADA", DbType.AnsiString, 1),
                new Column("MSGCODE_X3TIPOALCADA", DbType.AnsiString, 2),
                new Column("MSGCODE_X3ALCA1", DbType.AnsiString, 1),
                new Column("MSGCODE_X3ALCA2", DbType.AnsiString, 1),
                new Column("MSGCODE_X3ALCA3", DbType.AnsiString, 1),
                new Column("MSGCODE_X3ALCA4", DbType.AnsiString, 1),
                new Column("MSGCODE_X3ALCADAVLR", DbType.AnsiString, 1),
                new Column("MSGCODE_X4TIPO", DbType.AnsiString, 1),
                new Column("MSGCODE_X4ALCADA", DbType.AnsiString, 1),
                new Column("MSGCODE_X4TIPOALCADA", DbType.AnsiString, 2),
                new Column("MSGCODE_X4ALCA1", DbType.AnsiString, 1),
                new Column("MSGCODE_X4ALCA2", DbType.AnsiString, 1),
                new Column("MSGCODE_X4ALCA3", DbType.AnsiString, 1),
                new Column("MSGCODE_X4ALCA4", DbType.AnsiString, 1),
                new Column("MSGCODE_X4ALCADAVLR", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_MSGCODE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("MSGCODE");
            this.Database.RemoveSequence("SEQ_MSGCODE");
        }
    }
}
