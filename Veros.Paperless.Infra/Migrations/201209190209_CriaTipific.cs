namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190209)]
    public class CriaTipific : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "TIPIFIC",
                this.WithId("TIPIFIC_CODE"),
                new Column("TIPIFIC_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_TIPIFIC");
        }

        public override void Down()
        {
            this.Database.RemoveTable("TIPIFIC");
            this.Database.RemoveSequence("SEQ_TIPIFIC");
        }
    }
}
