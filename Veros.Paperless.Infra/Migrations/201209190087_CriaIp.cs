namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190087)]
    public class CriaIp : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "IP",
                this.WithId("IP_CODE"),
                new Column("IP_READERSRV", DbType.AnsiString, 16),
                new Column("IP_ICRSRV", DbType.AnsiString, 16),
                new Column("IP_TCPNI", DbType.AnsiString, 16),
                new Column("IP_TCPF1", DbType.AnsiString, 16),
                new Column("IP_TCPF2", DbType.AnsiString, 16),
                new Column("IP_TCPF3", DbType.AnsiString, 16),
                new Column("IP_TCPF4", DbType.AnsiString, 16),
                new Column("IP_TCPF5", DbType.AnsiString, 16),
                new Column("IP_TCPF6", DbType.AnsiString, 16),
                new Column("IP_TCPNDEP", DbType.AnsiString, 16),
                new Column("IP_TCPNPAG", DbType.AnsiString, 16),
                new Column("IP_TCPNMFORM", DbType.AnsiString, 16),
                new Column("IP_CHKSRV", DbType.AnsiString, 16),
                new Column("IP_HOIPSRV", DbType.AnsiString, 16),
                new Column("IP_DATABASE", DbType.AnsiString, 16));

            this.Database.CreateSequence("SEQ_IP");
        }

        public override void Down()
        {
            this.Database.RemoveTable("IP");
            this.Database.RemoveSequence("SEQ_IP");
        }
    }
}
