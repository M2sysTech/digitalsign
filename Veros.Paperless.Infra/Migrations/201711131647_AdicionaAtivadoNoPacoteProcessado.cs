namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201711131647)]
    public class AdicionaAtivadoNoPacoteProcessado : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("PACOTEPROCESSADO", new Column("PACOTEPROCESSADO_ATIVADO", DbType.AnsiString, 2));
        }

        public override void Down()
        {
        }
    }
}
