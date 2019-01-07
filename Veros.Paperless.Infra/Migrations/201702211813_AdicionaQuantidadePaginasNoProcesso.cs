namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201702211813)]
    public class AdicionaQuantidadePaginasNoProcesso : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PROC", new Column("PROC_QTDPAGINAS", DbType.AnsiString, 155));

            this.Database.AddColumn("PROC_BK", new Column("PROC_QTDPAGINAS", DbType.AnsiString, 155));

            this.Database.AddColumn("PROC_HIST", new Column("PROC_QTDPAGINAS", DbType.AnsiString, 155));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("PROC", "PROC_QTDPAGINAS");

            this.Database.RemoveColumn("PROC_BK", "PROC_QTDPAGINAS");

            this.Database.RemoveColumn("PROC_HIST", "PROC_QTDPAGINAS");
        }
    }
}
