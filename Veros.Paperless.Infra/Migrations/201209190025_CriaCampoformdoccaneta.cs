namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190025)]
    public class CriaCampoformdoccaneta : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CAMPOFORMDOCCANETA",
                this.WithId("CAMPOFORMDOCCANETA_CODE"),
                new Column("CAMPO_ID", DbType.Int32, ColumnProperty.NotNull),
                new Column("CAMPO_NOME", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("CAMPO_TEXTO", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("CAMPO_ORDEM", DbType.Int32, ColumnProperty.NotNull),
                new Column("CAMPO_OBRIGATORIO", DbType.Int32),
                new Column("CAMPO_VISIVEL", DbType.Int32),
                new Column("CAMPO_LARGURA", DbType.Int32),
                new Column("CAMPO_ALTURA", DbType.Int32),
                new Column("CAMPO_TIPO_ID", DbType.Int32),
                new Column("CAMPO_QTDIGITO", DbType.Int32),
                new Column("CAMPO_MASCARA", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_CAMPOFORMDOCCANETA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CAMPOFORMDOCCANETA");
            this.Database.RemoveSequence("SEQ_CAMPOFORMDOCCANETA");
        }
    }
}
