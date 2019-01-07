namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201306251521)]
    public class AdicionarObservacaoArquivoPacks : Migration
    {
        public override void Up()
        {                                                      
            this.Database.AddColumn("ARQUIVOPACK", new Column("ARQUIVOPACK_OBSERVACAO", DbType.AnsiString, 512));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("ARQUIVOPACK", "ARQUIVOPACK_OBSERVACAO");
        }
    }
}
