namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201805231654)]
    public class AdicionaColetaNoDossieEsperado : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("DOSSIEESPERADO", new Column("COLETA_CODE", DbType.Int32));
        }

        public override void Down()
        {
        }
    }
}
