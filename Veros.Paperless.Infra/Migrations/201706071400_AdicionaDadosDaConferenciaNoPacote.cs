namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201706071400)]
    public class AdicionaDadosDaConferenciaNoPacote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PACK", new Column("PACK_DTCONFERENCIA", DbType.Date));
            this.Database.AddColumn("PACK", new Column("USR_CONFERENCIA", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("PACK", "PACK_DTCONFERENCIA");
            this.Database.RemoveColumn("PACK", "USR_CONFERENCIA");
        }
    }
}
