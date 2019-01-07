namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201305141123)]
    public class CriaExpurgoLog : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "EXPURGOLOG",
                this.WithId("EXPURGOLOG_CODE"),
                new Column("EXPURGOLOG_MENSAGEM", DbType.String, 255),
                new Column("EXPURGOLOG_DATAHORA", DbType.DateTime));

            this.Database.CreateSequence("SEQ_EXPURGOLOG");
        }

        public override void Down()
        {
            this.Database.RemoveTable("EXPURGOLOG");
            this.Database.RemoveSequence("SEQ_EXPURGOLOG");
        }
    }
}
