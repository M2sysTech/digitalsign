namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201706051644)]
    public class CriaEmailPendente : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "EMAILPENDENTE",
                this.WithId("EMAILPENDENTE_CODE"),
                new Column("EMAILPENDENTE_DE", DbType.String, 254),
                new Column("EMAILPENDENTE_PARA", DbType.String, 254),
                new Column("EMAILPENDENTE_TIPONOTIFICACAO", DbType.Int32),
                new Column("EMAILPENDENTE_STATUS", DbType.Int32),
                new Column("EMAILPENDENTE_DT", DbType.Date),
                new Column("EMAILPENDENTE_ENVIAEM", DbType.Int32),
                new Column("EMAILPENDENTE_DESCRICAO", DbType.String, 1000),
                new Column("BATCH_CODE", DbType.Int32),
                new Column("PROC_CODE", DbType.Int32));

            this.Database.CreateSequence("SEQ_EMAILPENDENTE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("EMAILPENDENTE");
            this.Database.RemoveSequence("SEQ_EMAILPENDENTE");
        }
    }
}