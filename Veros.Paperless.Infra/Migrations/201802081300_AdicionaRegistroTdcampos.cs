namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201802081300)]
    public class AdicionaRegistroTdcampos : Migration
    {
        public override void Up()
        {
            this.Database.ExecuteNonQuery("INSERT INTO tdcampos (TDCAMPOS_CODE, TDCAMPOS_ID, TYPEDOC_ID, TDCAMPOS_DESC, TDCAMPOS_POSIMG,TDCAMPOS_POSDIG ) VALUES (1, 1, 990, 'Palavras OCR', '0;0;1;1;', '0;0')");
        }

        public override void Down()
        {
        }
    }
}
