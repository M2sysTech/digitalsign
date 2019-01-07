namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201706061049)]
    public class CriaDossieEsperado : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "DOSSIEESPERADO",
                this.WithId("DOSSIEESPERADO_CODE"),
                new Column("DOSSIEESPERADO_HIPOTECA", DbType.AnsiString, 255),
                new Column("DOSSIEESPERADO_MATRICULAAGENTE", DbType.AnsiString, 50),
                new Column("DOSSIEESPERADO_NOMEMUTUARIO", DbType.AnsiString, 255),
                new Column("DOSSIEESPERADO_NOMECONTRATO", DbType.AnsiString, 50),
                new Column("DOSSIEESPERADO_SITUACAO", DbType.AnsiString, 50),
                new Column("DOSSIEESPERADO_UFARQUIVO", DbType.AnsiString, 2),
                new Column("DOSSIEESPERADO_CODIGOBARRAS", DbType.AnsiString, 255),
                new Column("DOSSIEESPERADO_STATUS", DbType.AnsiString, 2),
                new Column("PACOTE_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_DOSSIEESPERADO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("DOSSIEESPERADO");
            this.Database.RemoveSequence("SEQ_DOSSIEESPERADO");
        }
    }
}
