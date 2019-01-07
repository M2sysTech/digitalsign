namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201708021100)]
    public class InsereEAtualizaTagQualidade : Migration
    {
        public override void Up()
        {
            this.AtualizarTagQualidadeGenericaParaM2();
            this.AdicionarTagQualidadeCef();
        }

        public override void Down()
        {
        }

        private void AtualizarTagQualidadeGenericaParaM2()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='QUALIDADE_PORCENTAGEM'").ToInt();

            if (count > 0)
            {
                this.Database.ExecuteNonQuery(@"
update tag set tag_desc = 'QUALIDADE_PORCENTAGEM_M2', 
tag_chave = 'QUALIDADE_PORCENTAGEM_M2', 
tag_value = '10'
where tag_chave = 'QUALIDADE_PORCENTAGEM'");
            }
        }

        private void AdicionarTagQualidadeCef()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='QUALIDADE_PORCENTAGEM_CEF'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'QUALIDADE_PORCENTAGEM_CEF', '3', 'QUALIDADE_PORCENTAGEM_CEF')");
            }
        }        
    }
}