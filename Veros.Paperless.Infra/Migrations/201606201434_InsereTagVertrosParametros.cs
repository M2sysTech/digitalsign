namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201606201434)]
    public class InsereTagVertrosParametros : Migration
    {
        public override void Up()
        {
            this.AdicionarTagVertrosUrl();
            this.AdicionarTagVertrosUsuario();
            this.AdicionarTagVertrosSenha();
            this.AdicionarTagVertrosApiKey();
        }

        public override void Down()
        {
        }

        private void AdicionarTagVertrosApiKey()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='vertros.usuario'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'vertros.usuario', 'm2sys', 'vertros.usuario')");
            }
        }

        private void AdicionarTagVertrosSenha()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='vertros.senha'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'vertros.senha', '117F,s2D5cJEU0<r/0Q', 'vertros.senha')");
            }
        }

        private void AdicionarTagVertrosUsuario()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='vertros.apikey'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'vertros.apikey', '0D3CA2BF-507C-48F9-8470-9801ADC8250F', 'vertros.apikey')");
            }
        }

        private void AdicionarTagVertrosUrl()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='vertros.url'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'vertros.url', 'http://177.8.160.29:6000/vchecker/vchecker.svc', 'vertros.url')");
            }
        }
    }
}