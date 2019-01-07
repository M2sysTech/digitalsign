namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201412181759)]
    public class InsereTagDiretorioDigitalizacao : Migration
    {
        public override void Up()
        {
            var count = this.Database
               .ExecuteScalar("select count(*) from tag where tag_chave='recepcao.diretorio.digitalizacao'")
               .ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'recepcao.diretorio.digitalizacao', 'C:\inetpub\ftproot\IMGCAP', 'recepcao.diretorio.digitalizacao')");
            }
        }

        public override void Down()
        {
        }
    }
}