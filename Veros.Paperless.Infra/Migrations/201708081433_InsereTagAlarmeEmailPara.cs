namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201708081433)]
    public class InsereTagAlarmeEmailPara : Migration
    {
        public override void Up()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='alarme.email.para'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'alarme.email.para', 'monitoramento@m2sys.com.br', 'alarme.email.para')");
            }
        }

        public override void Down()
        {
        }
    }
}