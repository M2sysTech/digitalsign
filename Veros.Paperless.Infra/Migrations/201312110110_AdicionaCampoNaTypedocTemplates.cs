namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201312110110)]
    public class AdicionaCampoNaTypedocTemplates : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("TYPEDOCTEMPLATE", new Column("TDCAMPOS_CODE", DbType.Int16));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("TYPEDOCTEMPLATE", "TDCAMPOS_CODE");
        }
    }
}