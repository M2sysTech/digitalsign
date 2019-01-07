namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190033)]
    public class CriaCidades : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CIDADES",
                this.WithId("CIDADES_CODE"),
                new Column("CD_CIDADE", DbType.Int32, ColumnProperty.NotNull),
                new Column("DS_CIDADE_NOME", DbType.AnsiString, 80, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_CIDADES");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CIDADES");
            this.Database.RemoveSequence("SEQ_CIDADES");
        }
    }
}
