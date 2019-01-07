namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190040)]
    public class CriaConfigfases : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CONFIGFASES",
                this.WithId("CONFIGFASES_CODE"),
                new Column("CONFIGFASES_OCR", DbType.AnsiString, 1),
                new Column("CONFIGFASES_IDENTIFICACAO", DbType.AnsiString, 1),
                new Column("CONFIGFASES_MONTAGEM", DbType.AnsiString, 1),
                new Column("CONFIGFASES_DIGITACAO", DbType.AnsiString, 1),
                new Column("CONFIGFASES_CONSULTA", DbType.AnsiString, 1),
                new Column("CONFIGFASES_VALIDACAO", DbType.AnsiString, 1),
                new Column("CONFIGFASES_PROVAZERO", DbType.AnsiString, 1),
                new Column("CONFIGFASES_FORMALISTICA", DbType.AnsiString, 1),
                new Column("CONFIGFASES_APROVACAO", DbType.AnsiString, 1),
                new Column("CONFIGFASES_AJUSTEORIGEM", DbType.AnsiString, 1),
                new Column("CONFIGFASES_EXPORTACAO", DbType.AnsiString, 1),
                new Column("CONFIGFASES_REMESSA", DbType.AnsiString, 1),
                new Column("CONFIGFASES_RETORNO", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_CONFIGFASES");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CONFIGFASES");
            this.Database.RemoveSequence("SEQ_CONFIGFASES");
        }
    }
}
