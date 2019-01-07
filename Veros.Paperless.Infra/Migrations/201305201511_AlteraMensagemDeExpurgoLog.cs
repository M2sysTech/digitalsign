namespace Veros.Paperless.Infra.Migrations
{
    using Data;
    using Migrator.Framework;

    [Migration(201305201511)]
    public class AlteraMensagemDeExpurgoLog : Migration
    {
        public override void Up()
        {
            this.Database.SetColumnAsChar4000("EXPURGOLOG", "EXPURGOLOG_MENSAGEM");
        }

        public override void Down()
        {
        }
    }
}