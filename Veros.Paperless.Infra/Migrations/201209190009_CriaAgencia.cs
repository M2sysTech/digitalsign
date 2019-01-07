namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190009)]
    public class CriaAgencia : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "AGENCIA",
                this.WithId("AGENCIA_CODE"),
                new Column("BANCO_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("MUNIC_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("AGENCIA_NUM", DbType.AnsiString, 4, ColumnProperty.NotNull),
                new Column("AGENCIA_DV", DbType.AnsiString, 1),
                new Column("AGENCIA_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("AGENCIA_ENDERECO", DbType.AnsiString, 127),
                new Column("AGENCIA_BAIRRO", DbType.AnsiString, 127),
                new Column("AGENCIA_CIDADE", DbType.AnsiString, 127),
                new Column("AGENCIA_UF", DbType.AnsiString, 2),
                new Column("AGENCIA_CEP", DbType.AnsiString, 16),
                new Column("AGENCIA_CONTATO", DbType.AnsiString, 127),
                new Column("AGENCIA_TELEFONE", DbType.AnsiString, 127),
                new Column("AGENCIA_OBS", DbType.AnsiString, 4000),
                new Column("AGENCIA_IP", DbType.AnsiString, 127),
                new Column("AGENCIA_CENTR", DbType.AnsiString, 1),
                new Column("AGENCIA_CENTCODE", DbType.Int32),
                new Column("AGENCIA_NSUINI", DbType.AnsiString, 12),
                new Column("AGENCIA_NSUFIM", DbType.AnsiString, 12),
                new Column("AGENCIA_LASTNSU", DbType.AnsiString, 12),
                new Column("AGENCIA_COMPE1", DbType.AnsiString, 3),
                new Column("AGENCIA_COMPE2", DbType.AnsiString, 3),
                new Column("AGENCIA_COMPE3", DbType.AnsiString, 3),
                new Column("AGENCIA_IDTERM", DbType.Int32),
                new Column("AGENCIA_SEQAUT", DbType.Int32),
                new Column("AGENCIA_ST", DbType.AnsiString, 1),
                new Column("AGENCIA_HORACORT", DbType.AnsiString, 5),
                new Column("AGENCIA_PATHLOC", DbType.AnsiString, 127),
                new Column("AGENCIA_LASTIMG", DbType.Int32),
                new Column("RERET_CODE", DbType.Int32),
                new Column("AGENCIA_CXABERTO", DbType.AnsiString, 127),
                new Column("AGENCIA_TRANSP", DbType.AnsiString, 1),
                new Column("AGENCIA_PATHCACH", DbType.AnsiString, 127),
                new Column("AGENCIA_BDU", DbType.AnsiString, 7),
                new Column("AGENCIA_HORAINIC", DbType.AnsiString, 5),
                new Column("AGENCIA_HORAFIM", DbType.AnsiString, 5),
                new Column("AGENCIA_XMAPA1", DbType.Int32),
                new Column("AGENCIA_YMAPA1", DbType.Int32),
                new Column("AGENCIA_XMAPA2", DbType.Int32),
                new Column("AGENCIA_YMAPA2", DbType.Int32),
                new Column("AGENCIA_DOCSMED", DbType.Int32),
                new Column("AGENCIA_DOCSLAST", DbType.Int32),
                new Column("AGENCIA_DOCTAXA", DbType.Int32),
                new Column("AGENCIA_DOCATUAL", DbType.Int32),
                new Column("REGIAO_CODE", DbType.Int32),
                new Column("AGENCIA_NUMOCORR", DbType.Int32),
                new Column("AGENCIA_ALCCRED", DbType.Decimal),
                new Column("AGENCIA_ALCDEB", DbType.Decimal),
                new Column("AGENCIA_ALCDOC", DbType.Decimal),
                new Column("AGENCIA_ALCIND", DbType.Decimal),
                new Column("AGENCIA_CELAPROV", DbType.AnsiString, 1),
                new Column("AGENCIA_CELDIGIT", DbType.AnsiString, 1),
                new Column("AGENCIA_ID", DbType.AnsiString, 16),
                new Column("AGENCIA_MULTICAP", DbType.AnsiString, 1),
                new Column("GRUPOMON_CODE", DbType.Int32),
                new Column("TIPIFIC_CODE", DbType.Int32),
                new Column("PREFERENC_CODE", DbType.Int32),
                new Column("AGENCIA_SCRPRI", DbType.Int32),
                new Column("AGENCIA_SCRSEC", DbType.Int32),
                new Column("AGENCIA_HORAFIMRMO", DbType.AnsiString, 5),
                new Column("AGENCIA_CAPINI", DbType.AnsiString, 5),
                new Column("AGENCIA_CAPFIM", DbType.AnsiString, 5),
                new Column("AGENCIA_DBID", DbType.AnsiString, 3),
                new Column("PREST_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_AGENCIA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("AGENCIA");
            this.Database.RemoveSequence("SEQ_AGENCIA");
        }
    }
}
