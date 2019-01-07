namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190109)]
    public class CriaLogremessa : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGREMESSA",
                this.WithId("LOGREMESSA_CODE"),
                new Column("LOGREMESSA_DATETIME", DbType.DateTime, ColumnProperty.NotNull),
                new Column("LOGREMESSA_DESC", DbType.AnsiString, 254),
                new Column("LOGREMESSA_ACTION", DbType.Int32),
                new Column("REMESSA_CODE", DbType.Int32, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_LOGREMESSA");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGREMESSA");
            this.Database.RemoveSequence("SEQ_LOGREMESSA");
        }
    }
}
