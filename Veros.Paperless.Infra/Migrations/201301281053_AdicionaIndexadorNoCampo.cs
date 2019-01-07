namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201301281053)]
    public class AdicionaIndexadorNoCampo : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("TDCAMPOS", new Column("TDCAMPOS_INDEXADOR", DbType.Boolean));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("TDCAMPOS", "TDCAMPOS_INDEXADOR");
        }
    }
}