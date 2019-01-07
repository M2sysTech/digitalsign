namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201307101134)]
    public class CriaImagem : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "IMAGEM",
                this.WithId("IMAGEM_CODE"),
                new Column("DOC_CODE", DbType.Int32),
                new Column("IMAGEM_ALTURA", DbType.Double),
                new Column("IMAGEM_LARGURA", DbType.Double),
                new Column("IMAGEM_RHORIZONTAL", DbType.Double),
                new Column("IMAGEM_RVERTICAL", DbType.Double),
                new Column("IMAGEM_TAMANHO", DbType.Int64),
                new Column("IMAGEM_CORES", DbType.Int32),
                new Column("IMAGEM_FORMATO", DbType.Int32));

            this.Database.CreateSequence("SEQ_IMAGEM");
        }

        public override void Down()
        {
            this.Database.RemoveTable("IMAGEM");
            this.Database.RemoveSequence("SEQ_IMAGEM");
        }
    }
}
