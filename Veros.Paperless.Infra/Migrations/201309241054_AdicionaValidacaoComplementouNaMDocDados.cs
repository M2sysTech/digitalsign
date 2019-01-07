namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201309241054)]
    public class AdicionaValidacaoComplementouNaMDocDados : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("MDOCDADOS", new Column("MDOCDADOS_VALIDCOMPLEMENTOU", DbType.Boolean));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("MDOCDADOS", "MDOCDADOS_VALIDCOMPLEMENTOU");
        }
    }
}