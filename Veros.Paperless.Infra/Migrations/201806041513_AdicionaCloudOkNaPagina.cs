namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201806041513)]
    public class AdicionaCloudOkNaPagina : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("DOC", new Column("DOC_COULDOK", DbType.Boolean));
            this.Database.AddColumn("DOC", new Column("DOC_FTREMOVIDO", DbType.Boolean));
            this.Database.AddColumn("DOC", new Column("DOC_DTCENTEROLD", DbType.Int32));
        }

        public override void Down()
        {
        }
    }
}
