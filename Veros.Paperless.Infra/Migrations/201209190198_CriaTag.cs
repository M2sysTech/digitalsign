namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190198)]
    public class CriaTag : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TAG",
                this.WithId("TAG_CODE"),
                new Column("TAG_DESC", DbType.AnsiString, 16),
                new Column("TAG_VALUE", DbType.AnsiString, 300),
                new Column("TAG_CHAVE", DbType.AnsiString, 30));

            this.Database.CreateSequence("SEQ_TAG");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TAG");
            this.Database.RemoveSequence("SEQ_TAG");
        }
    }
}
