namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201607071800)]
    public class AdicionaTimeFaceNaPagina : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("DOC", new Column("DOC_TIMEFACE", DbType.DateTime));
            this.Database.AddColumn("DOC_BK", new Column("DOC_TIMEFACE", DbType.DateTime));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("DOC", "DOC_TIMEFACE");
            this.Database.RemoveColumn("DOC_BK", "DOC_TIMEFACE");
        }
    }
}
