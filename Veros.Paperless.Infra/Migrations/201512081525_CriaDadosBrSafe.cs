namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201512081525)]
    public class CriaDadosBrSafe : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "dadosbrsafe",
                this.WithId("dadosbrsafe_code"),
                new Column("mdoc_code", DbType.Int32),
                new Column("usr_code", DbType.Int32),
                new Column("cadastrado_em", DbType.DateTime),
                new Column("dado_pergunta", DbType.String, 1000),
                new Column("dado_resposta", DbType.String, 1000),
                new Column("dado_id", DbType.String, 500));

            this.Database.CreateSequence("seq_dadosbrsafe");
        }

        public override void Down()
        {
            this.Database.RemoveTable("dadosbrsafe");
            this.Database.RemoveSequence("seq_dadosbrsafe");
        }
    }
}
