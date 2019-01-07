namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201404301405)]
    public class AdicionaFaseRevisaoNaConfigFases : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("CONFIGFASES", new Column("CONFIGFASES_REVISAO", DbType.String, 1));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("CONFIGFASES", "CONFIGFASES_REVISAO");
        }
    }
}