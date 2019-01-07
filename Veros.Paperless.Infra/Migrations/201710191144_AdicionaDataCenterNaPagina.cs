namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201710191144)]
    public class AdicionaDataCenterNaPagina : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("DOC", new Column("DOC_DTCENTERID", DbType.Int32));
            this.Database.AddColumn("DOC_BK", new Column("DOC_DTCENTERID", DbType.Int32));
            this.Database.AddColumn("DOC_HIST", new Column("DOC_DTCENTERID", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("DOC", "DOC_DTCENTERID");
        }
    }
}
