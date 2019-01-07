namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201805211710)]
    public class CriaPendenciaColeta : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PENDENCIACOLETA",
                this.WithId("PENDCOL_CODE"),
                new Column("ARQVCOL_CODE", DbType.Int32),
                new Column("PENDCOL_TIPO", DbType.AnsiString, 3),
                new Column("PENDCOL_TEXTO", DbType.AnsiString, 500),
                new Column("PENDCOL_ORDEM", DbType.Int32),
                new Column("PENDCOL_PROCESSOCSV", DbType.AnsiString, 50),
                new Column("PENDCOL_FOLDERCSV", DbType.AnsiString, 50),
                new Column("PENDCOL_CAIXACSV", DbType.AnsiString, 50),
                new Column("PENDCOL_QTDDOSSIECSV", DbType.Int32),
                new Column("PENDCOL_PROCESSOBD", DbType.AnsiString, 50),
                new Column("PENDCOL_FOLDERBD", DbType.AnsiString, 50),
                new Column("PENDCOL_CAIXABD", DbType.AnsiString, 50),
                new Column("PENDCOL_QTDDOSSIEBD", DbType.Int32),
                new Column("COLETA_CODEBD", DbType.Int32),
                new Column("PENDCOL_STATUSBD", DbType.AnsiString, 50),
                new Column("PENDCOL_DATAANALISE", DbType.Date, ColumnProperty.NotNull));
            this.Database.CreateSequence("SEQ_PENDENCIACOLETA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PENDENCIACOLETA");
            this.Database.RemoveSequence("SEQ_PENDENCIACOLETA");
        }
    }
}
