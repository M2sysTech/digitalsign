namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190077)]
    public class CriaFormrealizada : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "FORMREALIZADA",
                this.WithId("FORMREALIZADA_CODE"),
                new Column("MDOC_IMG", DbType.Int32, ColumnProperty.NotNull),
                new Column("MDOC_INFO", DbType.Int32),
                new Column("FORMREALIZADA_RESULT", DbType.AnsiString, 1, ColumnProperty.NotNull),
                new Column("FORMALISTICA_CODE", DbType.Int32, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_FORMREALIZADA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("FORMREALIZADA");
            this.Database.RemoveSequence("SEQ_FORMREALIZADA");
        }
    }
}
