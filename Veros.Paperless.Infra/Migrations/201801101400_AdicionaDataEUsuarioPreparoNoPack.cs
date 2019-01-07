namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201801101400)]
    public class AdicionaDataEUsuarioPreparoNoPack : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PACK", new Column("PACK_DTPREPARO", DbType.Date));
            this.Database.AddColumn("PACK", new Column("USR_PREPARO", DbType.Int32));
        }

        public override void Down()
        {
        }
    }
}
