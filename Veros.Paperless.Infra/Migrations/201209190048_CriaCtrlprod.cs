namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190048)]
    public class CriaCtrlprod : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CTRLPROD",
                this.WithId("CTRLPROD_CODE"),
                new Column("CTRLPROD_USR", DbType.Int32, ColumnProperty.NotNull),
                new Column("CTRLPROD_FASE", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("CTRLPROD_TOTAL", DbType.Int32, ColumnProperty.NotNull),
                new Column("CTRLPROD_IDACTION", DbType.AnsiString, 3, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_CTRLPROD");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CTRLPROD");
            this.Database.RemoveSequence("SEQ_CTRLPROD");
        }
    }
}
