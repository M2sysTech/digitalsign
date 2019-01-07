namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201706051625)]
    public class AdicionaDadosDaRecepcaoNaColeta : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("COLETA", new Column("COLETA_DTRECEPCAO", DbType.Date));
            this.Database.AddColumn("COLETA", new Column("USR_RECEP", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("COLETA", "COLETA_DTRECEPCAO");
            this.Database.RemoveColumn("COLETA", "USR_RECEP");
        }
    }
}
