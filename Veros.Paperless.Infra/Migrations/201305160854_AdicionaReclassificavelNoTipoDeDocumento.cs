namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Migrator.Framework;

    [Migration(201305160854)]
    public class AdicionaReclassificavelNoTipoDeDocumento : Migration
    {
        public override void Up()
        {
            this.Database.AddColumn("TYPEDOC", new Column("TYPEDOC_RECLASSIFICAVEL", DbType.Boolean));

            Database.ExecuteNonQuery("UPDATE typedoc SET typedoc_reclassificavel=0 WHERE TYPEDOC_PAC=1 OR TYPEDOC_ID IN (2,3,34,35,990)");
            Database.ExecuteNonQuery("UPDATE typedoc SET typedoc_reclassificavel=1 WHERE typedoc_reclassificavel IS NULL");
        }

        public override void Down()
        {
            this.Database.RemoveColumn("TYPEDOC", "TYPEDOC_RECLASSIFICAVEL");
        }
    }
}
