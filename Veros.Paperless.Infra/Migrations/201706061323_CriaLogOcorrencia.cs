namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201706061323)]
    public class CriaLogOcorrencia : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGOCORRENCIA",
                this.WithId("LOGOCORRENCIA_CODE"),
                new Column("LOGOCORRENCIA_DATAREGISTRO", DbType.Date),
                new Column("LOGOCORRENCIA_OBSERVACAO", DbType.AnsiString, 500),
                new Column("OCORRENCIA_CODE", DbType.Int32),
                new Column("USR_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_LOGOCORRENCIA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGOCORRENCIA");
            this.Database.RemoveSequence("SEQ_LOGOCORRENCIA");
        }
    }
}
