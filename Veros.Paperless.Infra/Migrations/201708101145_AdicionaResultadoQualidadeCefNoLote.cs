namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201708101145)]
    public class AdicionaResultadoQualidadeCefNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("BATCH_RESULTQCEF", DbType.AnsiString, 10));
            this.Database.AddColumn("BATCH_BK", new Column("BATCH_RESULTQCEF", DbType.AnsiString, 10));
            this.Database.AddColumn("BATCH_HIST", new Column("BATCH_RESULTQCEF", DbType.AnsiString, 10));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("BATCH", "BATCH_RESULTQCEF");
            this.Database.RemoveColumn("BATCH_BK", "BATCH_RESULTQCEF");
            this.Database.RemoveColumn("BATCH_HIST", "BATCH_RESULTQCEF");
        }
    }
}
