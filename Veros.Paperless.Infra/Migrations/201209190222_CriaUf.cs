namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190222)]
    public class CriaUf : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "UF",
                this.WithId("UF_CODE"),
                new Column("DS_UF_SIGLA", DbType.AnsiString, 20),
                new Column("DS_UF_NOME", DbType.AnsiString, 80));

            this.Database.CreateSequence("SEQ_UF");
        }

        public override void Down()
        {
            this.Database.RemoveTable("UF");
            this.Database.RemoveSequence("SEQ_UF");
        }
    }
}
