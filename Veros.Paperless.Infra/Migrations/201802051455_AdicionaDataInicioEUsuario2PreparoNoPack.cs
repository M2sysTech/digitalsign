namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201802051455)]
    public class AdicionaDataInicioEUsuario2PreparoNoPack : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PACK", new Column("PACK_DTINIPREPARO", DbType.Date));
            this.Database.AddColumn("PACK", new Column("USR_PREPARO2", DbType.Int32));
        }

        public override void Down()
        {
        }
    }
}
