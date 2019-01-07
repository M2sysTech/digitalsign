namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201606181649)]
    public class InsereTagDocumentoscopiaUrl : Migration
    {
        public override void Up()
        {
            this.AdicionarTagDocumentoscopiaUrl();
        }

        public override void Down()
        {
        }

        private void AdicionarTagDocumentoscopiaUrl()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='documentoscopia.url'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'documentoscopia.url', 'http://192.168.10.85', 'documentoscopia.url')");
            }
        }
    }
}