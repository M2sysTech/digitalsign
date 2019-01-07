namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201706221515)]
    public class AdicionaArquivoNaColeta : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("COLETA", new Column("COLETA_ARQUIVO", DbType.AnsiString, 50));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("COLETA", "COLETA_ARQUIVO");
        }
    }
}
