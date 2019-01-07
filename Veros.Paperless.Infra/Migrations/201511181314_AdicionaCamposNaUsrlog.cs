namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201511181314)]
    public class AdicionaCamposNaUsrlog : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("USRLOG", new Column("PERFIL_CODE", DbType.Int32));
            this.Database.AddColumn("USRLOG", new Column("USR_PAUSA", DbType.String, 1));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("USRLOG", "PERFIL_CODE");
            this.Database.RemoveColumn("USRLOG", "USR_PAUSA");
        }
    }
}