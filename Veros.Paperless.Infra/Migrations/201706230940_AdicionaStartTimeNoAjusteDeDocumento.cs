namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201706230940)]
    public class AdicionaStartTimeNoAjusteDeDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("AJUSTEMDOC", new Column("AJUSTMDOC_STARTTIME", DbType.Date));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("AJUSTEMDOC", "AJUSTMDOC_STARTTIME");
        }
    }
}
