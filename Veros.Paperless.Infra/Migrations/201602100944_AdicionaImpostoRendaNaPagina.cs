namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201602100944)]
    public class AdicionaImpostoRendaNaPagina : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("DOC", "DOC_IMPOSTORENDA", DbType.AnsiString, 10);
            this.Database.AddColumn("DOC_BK", "DOC_IMPOSTORENDA", DbType.AnsiString, 10);
            this.Database.AddColumn("DOC_HIST", "DOC_IMPOSTORENDA", DbType.AnsiString, 10);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("DOC", "DOC_IMPOSTORENDA");
            this.Database.RemoveColumn("DOC_BK", "DOC_IMPOSTORENDA");
            this.Database.RemoveColumn("DOC_HIST", "DOC_IMPOSTORENDA");
        }
    }
}
