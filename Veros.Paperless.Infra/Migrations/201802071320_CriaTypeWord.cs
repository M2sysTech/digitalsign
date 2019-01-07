namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Data;
    using Migrator.Framework;

    [Migration(201802071320)]
    public class CriaTypeWord : Migration
    {
        public override void Up()
        {
            this.Database.AddTable("TYPEWORD",
                this.WithId("TYPEWORD_CODE"),
                new Column("TYPEDOC_ID", DbType.Int32),
                new Column("TYPEWORD_TEXTO", DbType.AnsiString, 255),
                new Column("TYPEWORD_CATEGORIA", DbType.AnsiString, 2));

            this.Database.CreateSequence("SEQ_TYPEWORD");
        }

        public override void Down()
        {
        }
    }
}
