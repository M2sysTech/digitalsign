namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201708041012)]
    public class InsereRegraDeDocComProblemaNaClassificacao : Migration
    {
        public override void Up()
        {
            this.Database.ExecuteNonQuery("DELETE FROM REGRA WHERE REGRA_CODE=3");

            this.Database.ExecuteNonQuery(@"
INSERT INTO REGRA (
    REGRA_CODE, REGRA_IDENTIFICADOR, REGRA_DESC, REGRA_ATIVADA, REGRA_FASE, REGRA_CLASSIF, TYPEPROC_ID, REGRA_REVISAR, REGRA_PROCESSARMOTOR
    )VALUES ( 
    3, 'DPC', 'Documento com problema na classificação', 'S', 'Q', 'B', 1, 0, 'N')");
        }

        public override void Down()
        {
        }
    }
}