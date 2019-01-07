namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190093)]
    public class CriaIpmap : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "IPMAP",
                this.WithId("IPMAP_CODE"),
                new Column("IPMAP_IPNUMERIC", DbType.AnsiString, 127),
                new Column("IPMAP_DESC", DbType.AnsiString, 127),
                new Column("IPMAP_IPNUMBER", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_IPMAP");
        }

        public override void Down()
        {
            this.Database.RemoveTable("IPMAP");
            this.Database.RemoveSequence("SEQ_IPMAP");
        }
    }
}
