namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201305311352)]
    public class AdicionaGrupoCamoStyleNoCampo : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("TDCAMPOS", new Column("GRUPOCAMPO_STYLE", DbType.AnsiString, 512));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("TDCAMPOS", "GRUPOCAMPO_STYLE");
        }
    }
}
