namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190050)]
    public class CriaDbinfo : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "DBINFO",
                this.WithId("DBINFO_CODE"),
                new Column("DBINFO_VERSION", DbType.AnsiString, 5, ColumnProperty.NotNull),
                new Column("DBINFO_DATE", DbType.AnsiString, 127, ColumnProperty.NotNull));

            this.Database.CreateSequence("SEQ_DBINFO");
        }

        public override void Down()
        {
            this.Database.RemoveTable("DBINFO");
            this.Database.RemoveSequence("SEQ_DBINFO");
        }
    }
}
