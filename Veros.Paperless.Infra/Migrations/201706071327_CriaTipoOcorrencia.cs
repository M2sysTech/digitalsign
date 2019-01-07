namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201706071327)]
    public class CriaTipoOcorrencia : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TIPOOCORRENCIA",
                this.WithId("TIPOOCORRENCIA_CODE"),
                new Column("TIPOOCORRENCIA_NOME", DbType.AnsiString, 155));

            this.Database.CreateSequence("SEQ_TIPOOCORRENCIA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TIPOOCORRENCIA");
            this.Database.RemoveSequence("SEQ_TIPOOCORRENCIA");
        }
    }
}
