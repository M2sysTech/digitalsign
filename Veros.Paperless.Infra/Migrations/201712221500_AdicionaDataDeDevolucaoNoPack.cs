namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201712221500)]
    public class AdicionaDataDeDevolucaoNoPack : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PACK", new Column("PACK_DTDEVOLUCAO", DbType.Date));
        }

        public override void Down()
        {
        }
    }
}
