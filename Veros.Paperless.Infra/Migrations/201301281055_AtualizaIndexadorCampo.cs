namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201301281055)]
    public class AtualizaIndexadorCampo : Migration
    {
        public override void Up()
        {
            this.Database.ExecuteNonQuery("UPDATE TDCAMPOS SET TDCAMPOS_INDEXADOR = 1 WHERE TDCAMPOS_DESC IN ('CPF Documento','IDNDOC','CDDOC','STDOC','Sequencia Titular','Titular_Representante')");
            this.Database.ExecuteNonQuery("UPDATE TDCAMPOS SET TDCAMPOS_INDEXADOR = 0 WHERE TDCAMPOS_DESC NOT IN ('CPF Documento','IDNDOC','CDDOC','STDOC','Sequencia Titular','Titular_Representante')");
        }

        public override void Down()
        {
        }
    }
}