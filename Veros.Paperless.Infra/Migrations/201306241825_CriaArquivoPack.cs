namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201306241825)]
    public class CriaArquivoPack : Migration
    {
        public override void Up()
        {
            this.Database.AddTable("ARQUIVOPACK",
                this.WithId("ARQUIVOPACK_CODE"),
                new Column("PACOTEPROCESSADO_CODE", DbType.Int32),
                new Column("ARQUIVOPACK_DATA", DbType.DateTime),
                new Column("ARQUIVOPACK_NOMEPACOTE", DbType.AnsiString, 50),
                new Column("ARQUIVOPACK_TAMANHO", DbType.Int32),
                new Column("ARQUIVOPACK_STATUS", DbType.Int32));

            this.Database.CreateSequence("SEQ_ARQUIVOPACK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ARQUIVOPACK");
            this.Database.RemoveSequence("SEQ_ARQUIVOPACK");
        }
    }
}