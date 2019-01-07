namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190110)]
    public class CriaLogremessaBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGREMESSA_BK",
                this.WithId("LOGREMESSA_CODE"),
                new Column("LOGREMESSA_DATETIME", DbType.DateTime, ColumnProperty.NotNull),
                new Column("LOGREMESSA_DESC", DbType.AnsiString, 254),
                new Column("LOGREMESSA_ACTION", DbType.AnsiString, 3),
                new Column("REMESSA_CODE", DbType.Int32, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_LOGREMESSA_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGREMESSA_BK");
            this.Database.RemoveSequence("SEQ_LOGREMESSA_BK");
        }
    }
}
