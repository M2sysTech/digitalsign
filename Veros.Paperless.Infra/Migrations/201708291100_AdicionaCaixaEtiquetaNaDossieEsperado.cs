namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201708291100)]
    public class AdicionaCaixaEtiquetaNaDossieEsperado : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("DOSSIEESPERADO", new Column("DOSSIEESPERADO_CAIXAETIQUETA", DbType.AnsiString, 255));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("DOSSIEESPERADO", "DOSSIEESPERADO_CAIXAETIQUETA");
        }
    }
}
