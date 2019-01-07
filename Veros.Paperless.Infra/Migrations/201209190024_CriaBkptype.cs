namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190024)]
    public class CriaBkptype : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "BKPTYPE",
                this.WithId("BKPTYPE_CODE"),
                new Column("BKPTYPE_DESC", DbType.AnsiString, 127, ColumnProperty.NotNull),
                new Column("BKPTYPE_YEARS", DbType.Int32, ColumnProperty.NotNull),
                new Column("BKPTYPE_ACTIVE", DbType.AnsiString, 1, ColumnProperty.NotNull),
                new Column("BKPTYPE_SIZE", DbType.Decimal, ColumnProperty.NotNull),
                new Column("BKPTYPE_OBS", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_BKPTYPE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("BKPTYPE");
            this.Database.RemoveSequence("SEQ_BKPTYPE");
        }
    }
}
