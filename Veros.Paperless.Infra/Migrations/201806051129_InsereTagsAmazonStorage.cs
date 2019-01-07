namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201806051129)]
    public class InsereTagsAmazonStorage : Migration
    {
        public override void Up()
        {
            this.CriarTag("storage.amazon.accesskey", "AKIAJ7VXUYYJX5CTHE6Q");
            this.CriarTag("storage.amazon.secretkey", "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");
            this.CriarTag("storage.amazon.region", "sa-east-1");
            this.CriarTag("storage.amazon.bucket", "ftcef01");
        }

        public override void Down()
        {
        }

        private void CriarTag(string chave, string valor)
        {
            var sql = string.Format("select count(*) from tag where tag_chave='{0}'", chave);

            var count = this.Database.ExecuteScalar(sql).ToInt();

            if (count > 0)
            {
                return;
            }

            sql = string.Format(
                "insert into tag (tag_code, tag_desc, tag_value, tag_chave) values (seq_tag.nextval, '{0}', '{1}', '{0}')",
                chave,
                valor);

            this.Database.ExecuteNonQuery(sql);
        }
    }
}