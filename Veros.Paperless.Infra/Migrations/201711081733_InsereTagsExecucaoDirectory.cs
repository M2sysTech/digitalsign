namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201711081733)]
    public class InsereTagsExecucaoDirectory : Migration
    {
        public override void Up()
        {
            this.CriarTag("directory.inbox.pode.executar", "true");
            this.CriarTag("directory.outbox.pode.executar", "true");
            this.CriarTag("directory.inbox.posajuste.pode.executar", "true");
            this.CriarTag("directory.outbox.posajuste.pode.executar", "true");
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