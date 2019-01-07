namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201702270955)]
    public class InsereTagOcrTypedocs : Migration
    {
        public override void Up()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='OCR_TYPEDOCS'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'OCR_TYPEDOCS', '60,27', 'OCR_TYPEDOCS')");
            }
        }

        public override void Down()
        {
        }
    }
}