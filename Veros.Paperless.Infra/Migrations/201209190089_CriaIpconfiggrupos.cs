namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190089)]
    public class CriaIpconfiggrupos : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "IPCONFIGGRUPOS",
                this.WithId("IPCONFIGGRUPOS_CODE"),
                new Column("IPCONFIG_DTCENTERID", DbType.Int32, ColumnProperty.NotNull),
                new Column("IPCONFIG_TAG", DbType.AnsiString, 16, ColumnProperty.NotNull),
                new Column("IPCONFIG_IP", DbType.AnsiString, 16),
                new Column("IPCONFIG_PORT", DbType.Int32),
                new Column("IPCONFIG_DBID", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_IPCONFIGGRUPOS");
        }

        public override void Down()
        {
            this.Database.RemoveTable("IPCONFIGGRUPOS");
            this.Database.RemoveSequence("SEQ_IPCONFIGGRUPOS");
        }
    }
}
