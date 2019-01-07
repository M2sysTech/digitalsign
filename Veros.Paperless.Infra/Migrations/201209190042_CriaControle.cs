namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190042)]
    public class CriaControle : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "CONTROLE",
                this.WithId("CONTROLE_CODE"),
                new Column("CONTROLE_DTMOV", DbType.DateTime, ColumnProperty.NotNull),
                new Column("CONTROLE_DATAI", DbType.DateTime),
                new Column("CONTROLE_HORAI", DbType.AnsiString, 8),
                new Column("CONTROLE_USRI", DbType.AnsiString, 127),
                new Column("CONTROLE_DATAF", DbType.DateTime),
                new Column("CONTROLE_HORAF", DbType.AnsiString, 8),
                new Column("CONTROLE_USRF", DbType.AnsiString, 127),
                new Column("CONTROLE_OBS", DbType.AnsiString, 4000),
                new Column("CONTROLE_DURACAO", DbType.Int32),
                new Column("CONTROLE_DBID", DbType.AnsiString, 3));

            this.Database.CreateSequence("SEQ_CONTROLE");
        }

        public override void Down()
        {
            this.Database.RemoveTable("CONTROLE");
            this.Database.RemoveSequence("SEQ_CONTROLE");
        }
    }
}
