namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201606151446)]
    public class InsereTagConsultaPh3 : Migration
    {
        public override void Up()
        {
            this.AdicionarTagUrlConsultaPh3Url();
            this.AdicionarTagUrlAutenticacaoPh3Export();
        }

        public override void Down()
        {
        }

        private void AdicionarTagUrlConsultaPh3Url()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='consulta.ph3a.url'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'consulta.ph3a.url', 'http://hml.ws.databusca.com.br/DataXML.svc', 'consulta.ph3a.url')");
            }
        }

        private void AdicionarTagUrlAutenticacaoPh3Export()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='autenticacao.ph3.url'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'autenticacao.ph3.url', 'http://hml.ws.databusca.com.br/DataXML.svc', 'autenticacao.ph3.url')");
            }
        }
    }
}