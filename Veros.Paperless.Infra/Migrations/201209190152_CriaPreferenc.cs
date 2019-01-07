namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190152)]
    public class CriaPreferenc : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PREFERENC",
                this.WithId("PREFERENC_CODE"),
                new Column("PREFERENC_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_PREFERENC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PREFERENC");
            this.Database.RemoveSequence("SEQ_PREFERENC");
        }
    }
}
