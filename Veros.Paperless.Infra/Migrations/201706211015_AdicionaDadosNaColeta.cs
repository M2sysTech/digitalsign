namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201706211015)]
    public class AdicionaDadosNaColeta : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("COLETA", new Column("COLETA_UF", DbType.AnsiString, 2));
            this.Database.AddColumn("COLETA", new Column("TRANSP_CODE", DbType.Int32));            
        }

        public override void Down()
        {
            this.Database.RemoveColumn("COLETA", "COLETA_UF");
            this.Database.RemoveColumn("COLETA", "TRANSP_CODE");
        }
    }
}
