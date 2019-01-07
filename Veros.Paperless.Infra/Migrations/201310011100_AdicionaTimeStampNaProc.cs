namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201310011100)]
    public class AdicionaTimeStampNaProc : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PROC", new Column("PROC_TIMESTAMP", DbType.AnsiString, 50));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("PROC", "PROC_TIMESTAMP");
        }
    }
}