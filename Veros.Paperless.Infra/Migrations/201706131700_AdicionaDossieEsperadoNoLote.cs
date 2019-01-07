namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201706131700)]
    public class AdicionaDossieEsperadoNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("DOSSIEESPERADO_CODE", DbType.Int32));
            this.Database.AddColumn("BATCH_BK", new Column("DOSSIEESPERADO_CODE", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("BATCH", "DOSSIEESPERADO_CODE");
            this.Database.RemoveColumn("BATCH_BK", "DOSSIEESPERADO_CODE");
        }
    }
}
