namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201706161508)]
    public class ArrumaColunasAjusteDocumento : Migration
    {
        public override void Up()
        {
            this.Database.RemoveColumn("AJUSTEMDOC", "USR_CODE");
            this.Database.AddColumn("AJUSTEMDOC", new Column("USR_CODE", DbType.Int32));
        }

        public override void Down()
        {
        }
    }
}
