namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190019)]
    public class CriaBairros : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "BAIRROS",
                this.WithId("BAIRROS_CODE"),
                new Column("CD_CIDADE", DbType.Int32, ColumnProperty.NotNull),
                new Column("DS_BAIRRO_NOME", DbType.AnsiString, 80));

            this.Database.CreateSequence("SEQ_BAIRROS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("BAIRROS");
            this.Database.RemoveSequence("SEQ_BAIRROS");
        }
    }
}
