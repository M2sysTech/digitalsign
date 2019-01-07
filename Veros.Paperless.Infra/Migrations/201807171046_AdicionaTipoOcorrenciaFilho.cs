namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Framework;
    using Migrator.Framework;

    [Migration(201807171046)]
    public class AdicionaTipoOcorrenciaFilho : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("TIPOOCORRENCIA", new Column("TIPOOCORRENCIA_FILHOS", DbType.Int32));
            this.Database.AddColumn("OCORRENCIA", new Column("OCORRENCIA_CODEPAI", DbType.Int32));

            this.Database.ExecuteNonQuery("UPDATE TIPOOCORRENCIA SET TIPOOCORRENCIA_FILHOS=2 WHERE TIPOOCORRENCIA_CODE=2");
        }

        public override void Down()
        {
        }
    }
}