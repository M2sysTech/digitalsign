namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201301281232)]
    public class AdicionaControleAtalhosNoTipoDeDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("TYPEDOC", new Column("TYPEDOC_RANKING", DbType.Int32));
            this.Database.AddColumn("TYPEDOC", new Column("TYPEDOC_ATALHO", DbType.AnsiString, 2));
            this.Database.AddColumn("TYPEDOC", new Column("TYPEDOC_DESCLIMIT", DbType.AnsiString, 127));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("TYPEDOC", "TYPEDOC_RANKING");
            this.Database.RemoveColumn("TYPEDOC", "TYPEDOC_ATALHO");
            this.Database.RemoveColumn("TYPEDOC", "TYPEDOC_DESCLIMIT");
        }
    }
}