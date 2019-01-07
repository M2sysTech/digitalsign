namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201305061118)]
    public class AdicionarCamposRestantesParaBatchBk : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("BATCH_BK", new Column("batch_tbin", DbType.Date));
            this.Database.AddColumn("BATCH_BK", new Column("batch_tprz", DbType.Date));
            this.Database.AddColumn("BATCH_BK", new Column("batch_texport", DbType.Date));
            this.Database.AddColumn("BATCH_BK", new Column("batch_tmont", DbType.Date));
            this.Database.AddColumn("BATCH_BK", new Column("batch_tident", DbType.Date));
            this.Database.AddColumn("BATCH_BK", new Column("batch_tbatch_tmont", DbType.Date));
            this.Database.AddColumn("BATCH_BK", new Column("batch_tconsult", DbType.Date));
            this.Database.AddColumn("BATCH_BK", new Column("batch_tformal", DbType.Date));
            this.Database.AddColumn("BATCH_BK", new Column("batch_taprov", DbType.Date));
            this.Database.AddColumn("BATCH_BK", new Column("batch_tajustorig", DbType.Date));
            this.Database.AddColumn("BATCH_BK", new Column("batch_tenvio", DbType.Date));
            this.Database.AddColumn("BATCH_BK", new Column("batch_tretorno", DbType.Date));
            this.Database.AddColumn("BATCH_BK", new Column("batch_tajuste", DbType.Date));
            this.Database.AddColumn("BATCH_BK", new Column("pacoteprocessado_code", DbType.Int32));
            this.Database.AddColumn("BATCH_BK", new Column("faturamento_code", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("BATCH_BK", "batch_tbin");
            this.Database.RemoveColumn("BATCH_BK", "batch_tprz");
            this.Database.RemoveColumn("BATCH_BK", "batch_texport");
            this.Database.RemoveColumn("BATCH_BK", "batch_tmont");
            this.Database.RemoveColumn("BATCH_BK", "batch_tident");
            this.Database.RemoveColumn("BATCH_BK", "batch_tbatch_tmont");
            this.Database.RemoveColumn("BATCH_BK", "batch_tconsult");
            this.Database.RemoveColumn("BATCH_BK", "batch_tformal");
            this.Database.RemoveColumn("BATCH_BK", "batch_taprov");
            this.Database.RemoveColumn("BATCH_BK", "batch_tajustorig");
            this.Database.RemoveColumn("BATCH_BK", "batch_tenvio");
            this.Database.RemoveColumn("BATCH_BK", "batch_tretorno");
            this.Database.RemoveColumn("BATCH_BK", "batch_tajuste");
            this.Database.RemoveColumn("BATCH_BK", "pacoteprocessado_code");
            this.Database.RemoveColumn("BATCH_BK", "faturamento_code");
        }
    }
}
