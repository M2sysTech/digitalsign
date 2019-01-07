namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201708290815)]
    public class AdicionaDataCriacaoECodigoOcorrenciaNoTipoDeDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("TYPEDOC", new Column("TYPEDOC_DATACRIACAO", DbType.Date));
            this.Database.AddColumn("TYPEDOC", new Column("OCORRENCIA_CODE", DbType.Int32));
        }

        public override void Down()
        {
            this.Database.RemoveColumn("TYPEDOC", "TYPEDOC_DATACRIACAO");
            this.Database.RemoveColumn("TYPEDOC", "OCORRENCIA_CODE");
        }
    }
}
