namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190082)]
    public class CriaGrupomon : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "GRUPOMON",
                this.WithId("GRUPOMON_CODE"),
                new Column("GRUPOMON_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_GRUPOMON");
        }

        public override void Down()
        {
            this.Database.RemoveTable("GRUPOMON");
            this.Database.RemoveSequence("SEQ_GRUPOMON");
        }
    }
}
