namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190107)]
    public class CriaLogprod : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "LOGPROD",
                this.WithId("LOGPROD_CODE"),
                new Column("USR_CODE", DbType.Int32),
                new Column("LOGPROD_IDMODULO", DbType.Int32),
                new Column("LOGPROD_DTMOV", DbType.Int32),
                new Column("LOGPROD_CHEQUES", DbType.Int32),
                new Column("LOGPROD_NUMTOQUES", DbType.Int32),
                new Column("LOGPROD_TEMPODIG", DbType.Int32),
                new Column("LOGPROD_ESTACAO", DbType.AnsiString, 127),
                new Column("LOGPROD_ADRESS", DbType.AnsiString, 16),
                new Column("LOGPROD_DTINI", DbType.DateTime),
                new Column("LOGPROD_DTFIM", DbType.DateTime));

            this.Database.CreateSequence("SEQ_LOGPROD");
        }

        public override void Down()
        {
            this.Database.RemoveTable("LOGPROD");
            this.Database.RemoveSequence("SEQ_LOGPROD");
        }
    }
}
