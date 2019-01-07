namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201606161420)]
    public class CriaListaNegraCpf : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ListaNegraCpf",
                this.WithId("ListaNegraCpf_Code"),                                
                new Column("ListaNegraCpf_Numero", DbType.String, 20));

            this.Database.CreateSequence("seq_ListaNegraCpf");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ListaNegraCpf");
            this.Database.RemoveSequence("seq_ListaNegraCpf");
        }
    }
}
