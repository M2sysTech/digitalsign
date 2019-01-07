namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201708181515)]
    public class AdicionaResponsavelNaOcorrencia : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("OCORRENCIA", new Column("OCORRENCIA_USRRESP", DbType.Int32));
            this.Database.AddColumn("OCORRENCIA", new Column("OCORRENCIA_STARTTIME", DbType.Date));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("OCORRENCIA", "OCORRENCIA_USRRESP");
            this.Database.RemoveColumn("OCORRENCIA", "OCORRENCIA_STARTTIME");
        }
    }
}
