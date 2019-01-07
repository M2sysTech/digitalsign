namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190092)]
    public class CriaIpconfigTeste : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "IPCONFIG_TESTE",
                this.WithId("IPCONFIG_CODE"),
                new Column("IPCONFIG_DTCENTERID", DbType.Int32, ColumnProperty.NotNull),
                new Column("IPCONFIG_TAG", DbType.AnsiString, 16, ColumnProperty.NotNull),
                new Column("IPCONFIG_IP", DbType.AnsiString, 16),
                new Column("IPCONFIG_PORT", DbType.Int32),
                new Column("IPCONFIG_DBID", DbType.AnsiString, 127));

            this.Database.CreateSequence("SEQ_IPCONFIG_TESTE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("IPCONFIG_TESTE");
            this.Database.RemoveSequence("SEQ_IPCONFIG_TESTE");
        }
    }
}
