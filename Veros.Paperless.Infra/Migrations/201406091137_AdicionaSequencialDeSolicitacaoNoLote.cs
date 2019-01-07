namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201406091137)]
    public class AdicionaSequencialDeSolicitacaoNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", "BATCH_SOLSEQ", DbType.Int32);
            this.Database.AddColumn("BATCH_BK", "BATCH_SOLSEQ", DbType.Int32);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("BATCH", "BATCH_SOLSEQ");
            this.Database.RemoveColumn("BATCH_BK", "BATCH_SOLSEQ");
        }
    }
}
