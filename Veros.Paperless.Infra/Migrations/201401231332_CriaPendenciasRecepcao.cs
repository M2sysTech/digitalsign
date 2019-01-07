namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201401231332)]
    public class CriaPendenciasRecepcao : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "pendenciarecepcao",
                this.WithId("pendenciarecepcao_code"),
                this.WithId("solicitacao_id"),
                new Column("tentativas", DbType.Int32),
                new Column("ultimo_erro", DbType.String, 4000));

            this.Database.CreateSequence("seq_pendenciarecepcao");
        }

        public override void Down()
        {
            this.Database.RemoveTable("pendenciarecepcao");
            this.Database.RemoveSequence("seq_pendenciarecepcao");
        }
    }
}
