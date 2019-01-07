namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201401171440)]
    public class AlteraStatusEmRecepcao : Migration
    {
        public override void Up()
        {
            this.Database.ExecuteNonQuery("ALTER TABLE recepcao MODIFY (status NOT NULL)");
        }

        public override void Down()
        {
        }
    }
}
