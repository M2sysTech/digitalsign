namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201606091146)]
    public class AdicionaArquivoFotoNoUsuario : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("USR", "USR_ARQUIVOFOTO", DbType.AnsiString, 250);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("USR", "USR_ARQUIVOFOTO");
        }
    }
}
