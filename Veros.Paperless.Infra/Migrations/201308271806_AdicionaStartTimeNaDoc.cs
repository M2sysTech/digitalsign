namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201308271806)]
    public class AdicionaStartTimeNaDoc : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("DOC", new Column("DOC_STARTTIME", DbType.Date));
        }

        public override void Down()
        {
        }
    }
}
