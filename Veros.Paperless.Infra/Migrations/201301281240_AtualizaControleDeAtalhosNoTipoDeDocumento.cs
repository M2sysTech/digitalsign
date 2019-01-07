namespace Veros.Paperless.Infra.Migrations
{
    using Migrator.Framework;

    [Migration(201301281240)]
    public class AtualizaControleDeAtalhosNoTipoDeDocumento : Migration
    {
        public override void Up()
        {
            this.Database.ExecuteNonQuery("UPDATE TYPEDOC SET TYPEDOC_ATALHO = ROWNUM WHERE ROWNUM < 10 AND TYPEDOC_DESC NOT LIKE 'PAC%'");
            this.Database.ExecuteNonQuery("UPDATE TYPEDOC SET TYPEDOC_RANKING = 0");
            this.Database.ExecuteNonQuery("UPDATE TYPEDOC SET TYPEDOC_DESCLIMIT = SubStr(TYPEDOC_DESC,0,25)");
        }

        public override void Down()
        {
        }
    }
}