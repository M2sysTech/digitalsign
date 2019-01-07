namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201308301423)]
    public class CriaPalavraReconhecida : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PALAVRARECONHECIDA",
                this.WithId("PALAVRARECONHECIDA_CODE"),
                new Column("DOC_CODE", DbType.Int32),
                new Column("PALAVRARECONHECIDA_ALTURA", DbType.Int32),
                new Column("PALAVRARECONHECIDA_LARGURA", DbType.Int32),
                new Column("PALAVRARECONHECIDA_ESQUERDA", DbType.Int32),
                new Column("PALAVRARECONHECIDA_TOPO", DbType.Int32),
                new Column("PALAVRARECONHECIDA_DIREITA", DbType.Int32),
                new Column("PALAVRARECONHECIDA_TEXTO", DbType.AnsiString, 2000));

            this.Database.CreateSequence("SEQ_PALAVRARECONHECIDA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PALAVRARECONHECIDA");
            this.Database.RemoveSequence("SEQ_PALAVRARECONHECIDA");
        }
    }
}
