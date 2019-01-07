namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201506121529)]
    public class InsereTagDeProxy : Migration
    {
        public override void Up()
        {
            this.AdicionarTagDeveAutenticarProxy();
            this.AdicionarTagDeveUriProxy();
            this.AdicionarTagDeveUserProxy();
            this.AdicionarTagDevePasswordProxy();
            this.AdicionarTagDeveDominioProxy();
        }

        public override void Down()
        {
        }

        private void AdicionarTagDeveAutenticarProxy()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='consulta.proxy.autenticar'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'consulta.proxy.autenticar', 'true', 'consulta.proxy.autenticar')");
            }
        }

        private void AdicionarTagDeveUriProxy()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='consulta.proxy.uri'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'consulta.proxy.uri', 'http://192.168.10.18:3128', 'consulta.proxy.uri')");
            }
        }

        private void AdicionarTagDeveUserProxy()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='consulta.proxy.user'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'consulta.proxy.user', 'tiago', 'consulta.proxy.user')");
            }
        }

        private void AdicionarTagDevePasswordProxy()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='consulta.proxy.password'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'consulta.proxy.password', 'm2sys1@', 'consulta.proxy.password')");
            }
        }

        private void AdicionarTagDeveDominioProxy()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='consulta.proxy.dominio'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'consulta.proxy.dominio', 'verosit', 'consulta.proxy.dominio')");
            }
        }
    }
}