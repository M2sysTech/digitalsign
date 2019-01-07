namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190026)]
    public class CriaCartao : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CARTAO",
                this.WithId("CARTAO_CODE"),
                new Column("CARTAO_PREFIXO", DbType.AnsiString, 16, ColumnProperty.NotNull),
                new Column("CARTAO_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("CARTAO_OBS", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_CARTAO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CARTAO");
            this.Database.RemoveSequence("SEQ_CARTAO");
        }
    }
}
