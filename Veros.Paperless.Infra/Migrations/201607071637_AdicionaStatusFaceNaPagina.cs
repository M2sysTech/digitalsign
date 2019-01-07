namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201607071637)]
    public class AdicionaStatusFaceNaPagina : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("DOC", new Column("DOC_STATUSFACE", DbType.AnsiString, 1));
            this.Database.AddColumn("DOC_BK", new Column("DOC_STATUSFACE", DbType.AnsiString, 1));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("DOC", "DOC_STATUSFACE");
            this.Database.RemoveColumn("DOC_BK", "DOC_STATUSFACE");
        }
    }
}
