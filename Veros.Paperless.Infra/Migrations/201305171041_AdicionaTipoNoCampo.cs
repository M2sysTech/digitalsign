namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201305171041)]
    public class AdicionaTipoNoCampo : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("TDCAMPOS", new Column("TDCAMPOS_TIPO", DbType.Int32));

            Database.ExecuteNonQuery("UPDATE TDCAMPOS SET TDCAMPOS_TIPO = 1 WHERE TDCAMPOS_REFARQUIVO = 'NOMECLI'");
            Database.ExecuteNonQuery("UPDATE TDCAMPOS SET TDCAMPOS_TIPO = 0 WHERE TDCAMPOS_TIPO IS NULL");
        }

        public override void Down()
        {
            this.Database.RemoveColumn("TDCAMPOS", "TDCAMPOS_TIPO");
        }
    }
}
