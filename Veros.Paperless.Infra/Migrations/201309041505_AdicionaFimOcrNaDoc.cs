namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201309041505)]
    public class AdicionaFimOcrNaDoc : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("DOC", new Column("DOC_FIMOCR", DbType.Date));
        }

        public override void Down()
        {
        }
    }
}
