namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201802061000)]
    public class AdicionaUsageNaDll : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("DLL", new Column("DLL_OVERWRITE", DbType.Int32));
            this.Database.AddColumn("DLL", new Column("DLL_USAGE", DbType.AnsiString, 250));
            this.Database.ExecuteNonQuery("DELETE FROM DLL");
            this.Database.ExecuteNonQuery(" INSERT INTO DLL VALUES (SEQ_DLL.NEXTVAL, 'eng.cube.bigrams', '0', NULL,0, 'TESSDATA') ");
            this.Database.ExecuteNonQuery(" INSERT INTO DLL VALUES (SEQ_DLL.NEXTVAL, 'eng.cube.fold', '0', NULL,0, 'TESSDATA')");
            this.Database.ExecuteNonQuery(" INSERT INTO DLL VALUES (SEQ_DLL.NEXTVAL, 'eng.cube.lm', '0', NULL,0, 'TESSDATA')");
            this.Database.ExecuteNonQuery(" INSERT INTO DLL VALUES (SEQ_DLL.NEXTVAL, 'eng.cube.nn', '0', NULL,0, 'TESSDATA')");
            this.Database.ExecuteNonQuery(" INSERT INTO DLL VALUES (SEQ_DLL.NEXTVAL, 'eng.cube.params', '0', NULL,0, 'TESSDATA')");
            this.Database.ExecuteNonQuery(" INSERT INTO DLL VALUES (SEQ_DLL.NEXTVAL, 'eng.cube.size', '0',NULL,0, 'TESSDATA')");
            this.Database.ExecuteNonQuery(" INSERT INTO DLL VALUES (SEQ_DLL.NEXTVAL, 'eng.cube.word-freq', '0',NULL,0, 'TESSDATA')");
            this.Database.ExecuteNonQuery(" INSERT INTO DLL VALUES (SEQ_DLL.NEXTVAL, 'eng.tesseract_cube.nn', '0',NULL,0, 'TESSDATA')");
            this.Database.ExecuteNonQuery(" INSERT INTO DLL VALUES (SEQ_DLL.NEXTVAL, 'eng.traineddata', '0',NULL,0, 'TESSDATA')");
            this.Database.ExecuteNonQuery(" INSERT INTO DLL VALUES (SEQ_DLL.NEXTVAL, 'por.traineddata', '0',NULL,0, 'TESSDATA')");
        }

        public override void Down()
        {
        }
    }
}
