namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201312091700)]
    public class AdicionaChaveNaAcompProdUsr : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("ACOMPPRODUSR", new Column("ACOMPPRODUSR_CODE", DbType.Int64));
            this.Database.AddColumn("ACOMPPRODUSR_BK", new Column("ACOMPPRODUSR_CODE", DbType.Int64));
            this.Database.AddColumn("ACOMPPRODUSR_HIST", new Column("ACOMPPRODUSR_CODE", DbType.Int64));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("ACOMPPRODUSR", "ACOMPPRODUSR_CODE");
            this.Database.RemoveColumn("ACOMPPRODUSR_BK", "ACOMPPRODUSR_CODE");
            this.Database.RemoveColumn("ACOMPPRODUSR_HIST", "ACOMPPRODUSR_CODE");
        }
    }
}