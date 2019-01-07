namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201305031411)]
    public class AdicionaDecisaoEmProc : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn(
                "PROC",
                new Column("PROC_DECISAO", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("PROC", "PROC_DECISAO");
        }
    }
}