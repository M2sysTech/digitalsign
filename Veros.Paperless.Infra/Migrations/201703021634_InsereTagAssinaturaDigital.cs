namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201703021634)]
    public class InsereTagAssinaturaDigital : Migration
    {
        public override void Up()
        {
            this.InserirTag("assinaturadigital.author", "M2Sys Tecnologia da Informação");
            this.InserirTag("assinaturadigital.creator", "M2Sys Tecnologia da Informação");
            this.InserirTag("assinaturadigital.keywords", string.Empty);
            this.InserirTag("assinaturadigital.producer", "M2Sys Tecnologia da Informação");
            this.InserirTag("assinaturadigital.signaturecontact", "contato@m2sys.com.br");
            this.InserirTag("assinaturadigital.signaturelocation", "Rua Antônio R de Souza, 50 – Centro");
            this.InserirTag("assinaturadigital.signaturereason", "Segurança da Informação");
            this.InserirTag("assinaturadigital.subject", "Digitalização documentos CEF");
            this.InserirTag("assinaturadigital.title", "Digitalização documentos CEF");
        }

        public override void Down()
        {
        }

        private void InserirTag(string chave, string valor)
        {
            var sql = string.Format("select count(*) from tag where tag_chave='{0}'", chave);
            var count = this.Database.ExecuteScalar(sql).ToInt();

            if (count == 0)
            {
                var insertSql = string.Format(
                    "insert into tag (tag_code, tag_desc, tag_value, tag_chave) values (seq_tag.nextval, '{0}', '{1}', '{0}')",
                    chave,
                    valor);

                this.Database.ExecuteNonQuery(insertSql);
            }
        }
    }
}