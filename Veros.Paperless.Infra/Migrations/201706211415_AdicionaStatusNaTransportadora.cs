namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201706211415)]
    public class AdicionaStatusNaTransportadora : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("TRANSPORTADORA", new Column("TRANSP_STATUS", DbType.AnsiString, 1));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("TRANSPORTADORA", "TRANSP_STATUS");
        }
    }
}
