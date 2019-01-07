namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201803271700)]
    public class AdicionaMaisUsuariosPreparoNoPack : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PACK", new Column("USR_PREPARO3", DbType.Int32));
            this.Database.AddColumn("PACK", new Column("USR_PREPARO4", DbType.Int32));
        }

        public override void Down()
        {
        }
    }
}
