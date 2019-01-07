namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201303121411)]
    public class AdicionaReconhecivelEmTypeDoc : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("TYPEDOC", new Column("TYPEDOC_RECONHECIVEL", DbType.Boolean));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("TYPEDOC", "TYPEDOC_RECONHECIVEL");
        }
    }
}