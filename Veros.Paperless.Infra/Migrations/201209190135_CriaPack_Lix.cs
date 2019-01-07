namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190135)]
    public class CriaPackLix : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "PACK_LIX",
                this.WithId("PACK_CODE"),
                new Column("PACK_USRCAP", DbType.Int32, ColumnProperty.NotNull),
                new Column("PACK_HINI", DbType.DateTime),
                new Column("PACK_HFIM", DbType.DateTime),
                new Column("PACK_STATION", DbType.AnsiString, 127),
                new Column("PACK_STATUS", DbType.AnsiString, 1),
                new Column("PACK_BATIDO", DbType.AnsiString, 1),
                new Column("PACK_TOHOST", DbType.AnsiString, 1),
                new Column("PACK_FROMHOST", DbType.AnsiString, 1),
                new Column("PACK_TOTENV", DbType.Int32),
                new Column("PACK_AGENCIA", DbType.Int32),
                new Column("PACK_BDU", DbType.AnsiString, 7),
                new Column("PACK_DTMOV", DbType.DateTime));

            this.Database.CreateSequence("SEQ_PACK_LIX");
        }

        public override void Down()
        {
            this.Database.RemoveTable("PACK_LIX");
            this.Database.RemoveSequence("SEQ_PACK_LIX");
        }
    }
}
