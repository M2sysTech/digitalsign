namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201712181501)]
    public class AdicionaTriagemNaConfiguracaoDeFases : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("CONFIGFASES", new Column("CONFIGFASES_TRIAGEM", DbType.AnsiString, 1));

            this.Database.ExecuteNonQuery("UPDATE CONFIGFASES SET CONFIGFASES_TRIAGEM = 'S'");
        }

        public override void Down()
        {
        }
    }
}
