namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201702211623)]
    public class AdicionaIndexadoresNoProcesso : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PROC", new Column("PROC_IDENTIFICACAO", DbType.AnsiString, 155));
            this.Database.AddColumn("PACK", new Column("PACK_IDENTIFICACAO", DbType.AnsiString, 155));
            
            this.Database.AddColumn("PROC_BK", new Column("PROC_IDENTIFICACAO", DbType.AnsiString, 155));
            this.Database.AddColumn("PACK_BK", new Column("PACK_IDENTIFICACAO", DbType.AnsiString, 155));
            
            this.Database.AddColumn("PROC_HIST", new Column("PROC_IDENTIFICACAO", DbType.AnsiString, 155));
            this.Database.AddColumn("PACK_HIST", new Column("PACK_IDENTIFICACAO", DbType.AnsiString, 155));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("PROC", "PROC_IDENTIFICACAO");
            this.Database.RemoveColumn("PACK", "PACK_IDENTIFICACAO");
            
            this.Database.RemoveColumn("PROC_BK", "PROC_IDENTIFICACAO");
            this.Database.RemoveColumn("PACK_BK", "PACK_IDENTIFICACAO");
            
            this.Database.RemoveColumn("PROC_HIST", "PROC_IDENTIFICACAO");
            this.Database.RemoveColumn("PACK_HIST", "PACK_IDENTIFICACAO");
        }
    }
}
