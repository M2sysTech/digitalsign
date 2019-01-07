namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190081)]
    public class CriaGrupocampo : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "GRUPOCAMPO",
                this.WithId("GRUPOCAMPO_CODE"),
                new Column("GRUPOCAMPO_NAME", DbType.AnsiString, 250, ColumnProperty.NotNull),
                new Column("GRUPOCAMPO_ORDER", DbType.Int32));

            this.Database.CreateSequence("SEQ_GRUPOCAMPO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("GRUPOCAMPO");
            this.Database.RemoveSequence("SEQ_GRUPOCAMPO");
        }
    }
}
