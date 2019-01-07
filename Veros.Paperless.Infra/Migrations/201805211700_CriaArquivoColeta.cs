namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201805211700)]
    public class CriaArquivoColeta : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ARQUIVOCOLETA",
                this.WithId("ARQVCOL_CODE"),
                new Column("COLETA_CODE", DbType.Int32),
                new Column("USR_CODE", DbType.Int32, ColumnProperty.NotNull),
                new Column("ARQVCOL_DATETIME", DbType.Date, ColumnProperty.NotNull),
                new Column("ARQVCOL_NOME", DbType.AnsiString, 250),
                new Column("ARQVCOL_STATUS", DbType.AnsiString, 3),
                new Column("ARQVCOL_TAMANHOBYTES", DbType.Int32));
            this.Database.CreateSequence("SEQ_ARQUIVOCOLETA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ARQUIVOCOLETA");
            this.Database.RemoveSequence("SEQ_ARQUIVOCOLETA");
        }
    }
}
