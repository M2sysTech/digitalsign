namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201708111440)]
    public class AdicionaAcaoNoLogOcorrencia : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("LOGOCORRENCIA", new Column("LOGOCORRENCIA_ACAO", DbType.AnsiString, 10));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("LOGOCORRENCIA", "LOGOCORRENCIA_ACAO");
        }
    }
}
