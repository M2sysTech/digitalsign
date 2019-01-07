namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201807031112)]
    public class InsereTagsAmazonUrlEntryPoint : Migration
    {
        public override void Up()
        {
            this.CriarTag("storage.amazon.entrypoint", "https://{{0}}.s3{{1}}-accelerate.amazonaws.com/{{2}}/{{3}}");
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

            Log.Application.Debug(sql);

            this.Database.ExecuteNonQuery(sql);
        }
    }
}