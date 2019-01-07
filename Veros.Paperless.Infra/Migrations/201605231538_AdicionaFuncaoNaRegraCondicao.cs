namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201605231538)]
    public class AdicionaFuncaoNaRegraCondicao : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("REGRACOND", "REGRACOND_FUNCAO", DbType.AnsiString, 200);
        }

        public override void Down()
        {
            this.Database.RemoveColumn("REGRACOND", "REGRACOND_FUNCAO");
        }
    }
}
