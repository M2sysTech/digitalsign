namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201706141130)]
    public class AdicionaQualidadeNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("BATCH_QUALIM2", DbType.Int32));
            this.Database.AddColumn("BATCH", new Column("BATCH_QUALICEF", DbType.Int32));
            this.Database.AddColumn("BATCH_BK", new Column("BATCH_QUALIM2", DbType.Int32));
            this.Database.AddColumn("BATCH_BK", new Column("BATCH_QUALICEF", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("BATCH", "BATCH_QUALIM2");
            this.Database.RemoveColumn("BATCH", "BATCH_QUALICEF");
            this.Database.RemoveColumn("BATCH_BK", "BATCH_QUALIM2");
            this.Database.RemoveColumn("BATCH_BK", "BATCH_QUALICEF");
        }
    }
}
