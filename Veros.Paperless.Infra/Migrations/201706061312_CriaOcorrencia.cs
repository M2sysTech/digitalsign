namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201706061312)]
    public class CriaOcorrencia : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "OCORRENCIA",
                this.WithId("OCORRENCIA_CODE"),
                new Column("OCORRENCIA_DATAREGISTRO", DbType.Date),
                new Column("OCORRENCIA_OBSERVACAO", DbType.AnsiString, 500),
                new Column("OCORRENCIA_STATUS", DbType.AnsiString, 2),
                new Column("TIPOOCORRENCIA_CODE", DbType.Int32),
                new Column("MDOC_CODE", DbType.Int32),
                new Column("DOSSIEESPERADO_CODE", DbType.Int32),
                new Column("BATCH_CODE", DbType.Int32),
                new Column("PACOTE_CODE", DbType.Int32),
                new Column("USR_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_OCORRENCIA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("OCORRENCIA");
            this.Database.RemoveSequence("SEQ_OCORRENCIA");
        }
    }
}
