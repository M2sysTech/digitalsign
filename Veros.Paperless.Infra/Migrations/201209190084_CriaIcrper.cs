namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190084)]
    public class CriaIcrper : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ICRPER",
                this.WithId("ICRPER_CODE"),
                new Column("ICRPER_DATETIME", DbType.DateTime, ColumnProperty.NotNull),
                new Column("ICRPER_RECONH", DbType.Decimal),
                new Column("ICRPER_SEMDIGIT", DbType.Decimal),
                new Column("ICRPER_CMC7", DbType.Decimal),
                new Column("ICRPER_VALOR", DbType.Decimal),
                new Column("ICRPER_FALSOOCR", DbType.Decimal));

            this.Database.CreateSequence("SEQ_ICRPER");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ICRPER");
            this.Database.RemoveSequence("SEQ_ICRPER");
        }
    }
}
