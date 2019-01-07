namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201703021200)]
    public class AdicionaColetaNoPacote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PACK", new Column("COLETA_CODE", DbType.Int32));
            this.Database.AddColumn("PACK_BK", new Column("COLETA_CODE", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("PACK", "COLETA_CODE");
            this.Database.RemoveColumn("PACK_BK", "COLETA_CODE");
        }
    }
}
