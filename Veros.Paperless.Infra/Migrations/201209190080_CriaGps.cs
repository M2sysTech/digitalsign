namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190080)]
    public class CriaGps : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "GPS",
                this.WithId("GPS_CODE"),
                new Column("GPS_ID", DbType.AnsiString, 16, ColumnProperty.NotNull),
                new Column("GPS_DESC", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_GPS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("GPS");
            this.Database.RemoveSequence("SEQ_GPS");
        }
    }
}
