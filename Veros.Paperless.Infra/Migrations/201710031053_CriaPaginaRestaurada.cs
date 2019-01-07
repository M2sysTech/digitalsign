namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201710031053)]
    public class CriaPaginaRestaurada : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PAGINARESTAURADA",
                this.WithId("PAGINARESTAURADA_CODE"),
                new Column("DOC_CODE", DbType.Int32),
                new Column("MDOC_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_PAGINARESTAURADA");
        }

        public override void Down()
        {
        }
    }
}
