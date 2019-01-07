namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Framework;
    using Migrator.Framework;

    [Migration(201807110930)]
    public class InsereOcorrenciaDossieEsperado : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("DOSSIEESPERADO", new Column("OCORRENCIA_CODE", DbType.Int32));
        }

        public override void Down()
        {
        }
    }
}