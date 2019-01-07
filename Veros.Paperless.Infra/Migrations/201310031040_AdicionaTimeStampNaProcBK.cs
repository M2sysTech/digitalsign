namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201310031040)]
    public class AdicionaTimeStampNaProcBK : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PROC_BK", new Column("PROC_TIMESTAMP", DbType.AnsiString, 50));
            this.Database.AddColumn("PROC_HIST", new Column("PROC_TIMESTAMP", DbType.AnsiString, 50));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("PROC_BK", "PROC_TIMESTAMP");
            this.Database.RemoveColumn("PROC_HIST", "PROC_TIMESTAMP");
        }
    }
}