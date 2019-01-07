namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201303071529)]
    public class AdicionaCpfNaRegraProc : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REGRAPROC", new Column("REGRAPROC_CPF", DbType.AnsiString, 11));
        }

        public override void Down()
        {
        }
    }
}