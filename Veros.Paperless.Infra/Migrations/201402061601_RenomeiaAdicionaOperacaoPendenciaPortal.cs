namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201402061601)]
    public class RenomeiaAdicionaOperacaoPendenciaPortal : Migration
    {
        public override void Up()
        {
            this.Database.RenameTable("pendenciarecepcao", "pendenciaportal");
            this.Database.AddColumn("pendenciaportal", new Column("operacao", DbType.Byte));
            this.Database.ExecuteNonQuery("rename seq_pendenciarecepcao to seq_pendenciaportal");
        }

        public override void Down()
        {
        }
    }
}
