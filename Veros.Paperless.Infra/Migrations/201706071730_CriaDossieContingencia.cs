namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201706071730)]
    public class CriaDossieContingencia : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "DOSSIECONT",
                this.WithId("DOSSIECONT_CODE"),
                new Column("DOSSIECONT_HIPOTECA", DbType.AnsiString, 255),
                new Column("DOSSIECONT_MATRICULAAGENTE", DbType.AnsiString, 50),
                new Column("DOSSIECONT_NOMEMUTUARIO", DbType.AnsiString, 255),
                new Column("DOSSIECONT_NUMEROCONTRATO", DbType.AnsiString, 50),
                new Column("DOSSIECONT_SITUACAO", DbType.AnsiString, 50),
                new Column("DOSSIECONT_UFARQUIVO", DbType.AnsiString, 2),
                new Column("DOSSIECONT_CODIGOBARRAS", DbType.AnsiString, 255),
                new Column("DOSSIECONT_CAIXASEQ", DbType.Int32),
                new Column("DOSSIECONT_CAIXAETIQUETA", DbType.AnsiString, 255));

            this.Database.CreateSequence("SEQ_DOSSIECONT");
        }

        public override void Down()
        {
            this.Database.RemoveTable("DOSSIECONT");
            this.Database.RemoveSequence("SEQ_DOSSIECONT");
        }
    }
}
