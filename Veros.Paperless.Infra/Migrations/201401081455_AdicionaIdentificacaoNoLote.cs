namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201401081455)]
    public class AdicionaIdentificacaoNoLote : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH", new Column("BATCH_IDENTIFICACAO", DbType.String, 20));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("BATCH", "BATCH_IDENTIFICACAO");
        }
    }
}