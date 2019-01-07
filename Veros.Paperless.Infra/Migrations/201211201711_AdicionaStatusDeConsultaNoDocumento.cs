namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201211201711)]
    public class AdicionaStatusDeConsultaNoDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOC", new Column("MDOC_STATUSCONSULTA", DbType.AnsiString));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("MDOC", "MDOC_STATUSCONSULTA");
        }
    }
}