namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190062)]
    public class CriaEnvBk : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "ENV_BK",
                this.WithId("ENV_CODE"),
                new Column("BATCH_CODE", DbType.Int32),
                new Column("ENV_AGENCIA", DbType.AnsiString, 6),
                new Column("ENV_CONTA", DbType.AnsiString, 12),
                new Column("ENV_INFO", DbType.AnsiString, 127),
                new Column("ENV_CEP", DbType.AnsiString, 16),
                new Column("ENV_AUTENTIC", DbType.AnsiString, 1),
                new Column("USR_CODE", DbType.Int32),
                new Column("ENV_DATE", DbType.DateTime),
                new Column("ENV_TOTDOCS", DbType.Int32),
                new Column("ENV_STATUS", DbType.AnsiString, 1),
                new Column("ENV_TFIM", DbType.DateTime));

            this.Database.CreateSequence("SEQ_ENV_BK");
        }

        public override void Down()
        {
            this.Database.RemoveTable("ENV_BK");
            this.Database.RemoveSequence("SEQ_ENV_BK");
        }
    }
}
