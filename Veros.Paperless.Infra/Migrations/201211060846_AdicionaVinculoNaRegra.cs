namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201211060846)]
    public class AdicionaVinculoNaRegra : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REGRA", new Column("REGRA_VINCULO", DbType.AnsiString, 10));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("REGRA", "REGRA_VINCULO");
        }
    }
}