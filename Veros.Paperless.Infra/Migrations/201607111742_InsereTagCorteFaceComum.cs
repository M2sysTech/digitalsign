namespace Veros.Paperless.Infra.Migrations
{
    using Framework;
    using Migrator.Framework;

    [Migration(201607111742)]
    public class InsereTagCorteFaceComum : Migration
    {
        public override void Up()
        {
            var count = this.Database.ExecuteScalar("select count(*) from tag where tag_chave='COMPARATOR_CORTE_FACECOMUM'").ToInt();

            if (count == 0)
            {
                this.Database.ExecuteNonQuery(@"
insert into 
    tag 
(tag_code, tag_desc, tag_value, tag_chave)
    values
(seq_tag.nextval, 'COMPARATOR_CORTE_FACECOMUM', '10', 'COMPARATOR_CORTE_FACECOMUM')");
            }
        }

        public override void Down()
        {
        }
    }
}