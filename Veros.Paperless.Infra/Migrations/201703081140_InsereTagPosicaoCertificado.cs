namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201703081140)]
    public class InsereTagPosicaoCertificado : Migration
    {
        public override void Up()
        {
            this.InserirTag("assinaturadigital.posicaocertificado", "0");
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