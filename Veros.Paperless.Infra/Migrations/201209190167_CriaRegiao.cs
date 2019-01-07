namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190167)]
    public class CriaRegiao : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "REGIAO",
                this.WithId("REGIAO_CODE"),
                new Column("REGIAO_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("REGIAO_UF", DbType.AnsiString, 2),
                new Column("REGIAO_X", DbType.Int32),
                new Column("REGIAO_Y", DbType.Int32));

            this.Database.CreateSequence("SEQ_REGIAO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("REGIAO");
            this.Database.RemoveSequence("SEQ_REGIAO");
        }
    }
}
