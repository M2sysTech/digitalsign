namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201510071149)]
    public class InsereTagDePortal : Migration
    {
        public override void Up()
        {
            this.AdicionarTagPortalExportUrl();
            this.AdicionarTagPortalRecursoExport();
        }

        public override void Down()
        {
        }

        private void AdicionarTagPortalExportUrl()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='export.portal.url'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'export.portal.url', 'http://localhost:8090', 'export.portal.url')");
            }
        }

        private void AdicionarTagPortalRecursoExport()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='export.portal.recurso.exportar'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'export.portal.recurso.exportar', '/api/AberturaContaExport/Exportar', 'export.portal.recurso.exportar')");
            }
        }
    }
}