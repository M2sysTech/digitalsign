namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201305101530)]
    public class AdicionaOcrPodeInserirEmTdCampos : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn(
                "TDCAMPOS",
                new Column("TDCAMPOS_OCRPODEINSERIR", DbType.Boolean));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("TDCAMPOS", "TDCAMPOS_OCRPODEINSERIR");
        }
    }
}