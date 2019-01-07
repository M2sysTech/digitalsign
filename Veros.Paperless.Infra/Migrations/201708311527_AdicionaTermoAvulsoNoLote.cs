namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201708311527)]
    public class AdicionaTermoAvulsoNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("BATCH_TERMOPATCH", DbType.Boolean));
        }

        public override void Down()
        {
        }
    }
}
