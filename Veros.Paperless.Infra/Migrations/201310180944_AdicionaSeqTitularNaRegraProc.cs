namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201310180944)]
    public class AdicionaSeqTitularNaRegraProc : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REGRAPROC", new Column("REGRAPROC_SEQTIT", DbType.AnsiString, 3));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("REGRAPROC", "REGRAPROC_SEQTIT");
        }
    }
}