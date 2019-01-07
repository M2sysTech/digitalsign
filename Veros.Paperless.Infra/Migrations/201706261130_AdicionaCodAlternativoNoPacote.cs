namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201706261130)]
    public class AdicionaCodAlternativoNoPacote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PACK", new Column("PACK_CODALTERNATIVO", DbType.AnsiString, 155));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("PACK", "PACK_CODALTERNATIVO");
        }
    }
}
