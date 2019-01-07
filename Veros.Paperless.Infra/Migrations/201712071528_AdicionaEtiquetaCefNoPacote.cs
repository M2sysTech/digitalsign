namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201712071528)]
    public class AdicionaEtiquetaCefNoPacote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PACK", new Column("PACK_ETIQUETACEF", DbType.Boolean));
        }

        public override void Down()
        {
        }
    }
}
