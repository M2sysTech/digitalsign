namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201606161545)]
    public class AdicionaDocumentoscopiaNaTdCampos : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("TDCAMPOS", "TDCAMPOS_DOCUMENTOSCOPIA", DbType.AnsiString, 1);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("TDCAMPOS", "TDCAMPOS_DOCUMENTOSCOPIA");
        }
    }
}
